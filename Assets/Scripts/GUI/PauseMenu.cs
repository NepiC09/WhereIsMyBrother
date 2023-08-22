using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] private PlayerInput playerInput;

    [Header("GUI Panels")]
    [SerializeField] private GameObject[] GUIPanelList;

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
        PlayerController.Instance.setPlayable(true);
        canvasGroup.LeanAlpha(0, animationTime);
        LeanTween.scale(gameObject, Vector3.zero, animationTime).setOnComplete(() =>
        {
            gameObject.SetActive(false);
        });
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
        SaveSystemManager.Instance.SaveGame();
        ClosePauseMenu();
    }
    public void _LoadGameButtonPressed()
    {
        //SaveSystemManager.Instance.LoadGame();
        ClosePauseMenu();
        Fader.Instance.FadeInOut(1f, 0.5f, 1f, SaveSystemManager.Instance.LoadGame);
    }
}
