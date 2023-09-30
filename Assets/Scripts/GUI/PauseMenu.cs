using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private PlayerInput playerInput;

    [Header("GUI Panels")]
    [SerializeField] private GameObject[] GUIPanelList;
    [SerializeField] private AskPanel askPanel;

    [Header("Slots Panels")]
    [SerializeField] private ChooseSave SaveSlots;
    [SerializeField] private ChooseSave LoadSlots;

    private CanvasGroup canvasGroup;
    private float animationTime = 0.2f;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    private void Start()
    {
        playerInput.currentActionMap.FindAction("PauseButton").performed += PauseButtonPerfomed;
        gameObject.SetActive(false);
    }

    private void PauseButtonPerfomed(InputAction.CallbackContext o)
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;
        if (!Fader.Instance.isUnFaded())
            return;
        if (BubbleDialogue.isBubblePlaying)
            return;

        foreach(GameObject panel in GUIPanelList)
        {
            if (panel.activeSelf)
            {
                return;
            }
        }

        if (!gameObject.activeSelf)
        {
            OpenPauseMenu();
        }
        else
        {
            ClosePauseMenu();
        }
    }

    private void OpenPauseMenu()
    {
        PlayerController.Instance.setPlayable(false);
        transform.localScale = Vector3.zero;
        canvasGroup.alpha = 0;
        gameObject.SetActive(true);
        LeanTween.scale(gameObject, Vector3.one, animationTime);
        canvasGroup.LeanAlpha(1, animationTime);
    }
    private void ClosePauseMenu()
    {
        if (SaveSlots.transform.localScale == Vector3.zero && LoadSlots.transform.localScale == Vector3.zero)
        {
            PlayerController.Instance.setPlayable(true);
            canvasGroup.LeanAlpha(0, animationTime);
            LeanTween.scale(gameObject, Vector3.zero, animationTime).setOnComplete(() =>
            {
                gameObject.SetActive(false);
            });
        }
    }

    //TESTING
    public void _NewGameButtonPressed()
    {
        //SaveSystemManager.Instance.NewGame();
        ClosePauseMenu();
        Fader.Instance.FadeInOut(1f, 0.5f, 1f, SaveSystemManager.Instance.NewGame);
    }
    public void _SaveGameButtonPressed()
    {
        SaveSlots.OpenChooseSave();
    }
    public void _LoadGameButtonPressed()
    {
        LoadSlots.OpenChooseSave();
    }

    public void _QuitGameButtonPressed() {
        askPanel.setText("Сохранить игру?");
        askPanel.OpenAskPanel();
        askPanel.onAskChoiced.AddListener((bool isYes) => {
            askPanel.onAskChoiced.RemoveAllListeners();
            if (isYes)
            {
                GlobalScripts.currentSaveFileName = SaveSystemManager.Instance.fileName;
                SaveSystemManager.Instance.SaveGame(SaveSystemManager.Instance.fileName);
            }
            Application.Quit();
        });
    }

    public void _MainMenuButtonPressed() {
        askPanel.setText("Сохранить игру?");
        askPanel.OpenAskPanel();
        askPanel.onAskChoiced.AddListener((bool isYes) => {
            askPanel.onAskChoiced.RemoveAllListeners();
            if (isYes)
            {
                GlobalScripts.currentSaveFileName = SaveSystemManager.Instance.fileName;
                SaveSystemManager.Instance.SaveGame(SaveSystemManager.Instance.fileName);
            }
            Fader.Instance.GetComponent<CanvasGroup>().LeanAlpha(1f, 0.25f).setOnComplete(() =>
            {
                SceneManager.LoadScene(0);
                //GlobalScripts.isStartedNewGame = false;
            });
        });
    }
}
