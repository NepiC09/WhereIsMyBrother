using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneEntry : MonoBehaviour, IDataPersistance
{
    [SerializeField] private TextMeshProUGUI codeLabel;
    [SerializeField] private GameObject MessageBox;
    [SerializeField] private PlayerInput playerInput;

    private const string RIGHT_CODE = "190808";
    public bool isFisrtOpenedMainScreen = false;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void LoadData(GameData gameData)
    {
        isFisrtOpenedMainScreen = gameData.phoneEntry_isFisrtOpenedMainScreen;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.phoneEntry_isFisrtOpenedMainScreen = isFisrtOpenedMainScreen;
    }

    public void OpenEntryScreen()
    {
        _ResetCodeLabel();
        gameObject.SetActive(true);
        transform.localScale = Vector3.one;
        canvasGroup.alpha = 1f;
    }

    public void _AddNumberCodeLabel(int number)
    {
        codeLabel.text += number.ToString();
        CheckCode();
    }

    public void _ResetCodeLabel() {
        codeLabel.text = "";
    }

    private void CheckCode()
    {
        if(codeLabel.text == RIGHT_CODE)
        {
            isFisrtOpenedMainScreen = true;
            ClosePhoneEntry();
        }
        if(codeLabel.text.Length == 6)
        {
            ShowMessageBox();
        }
    }

    private void ShowMessageBox()
    {
        MessageBox.SetActive(true);

        float timeToShow = 0.25f;

        LeanTween.alphaCanvas(MessageBox.GetComponent<CanvasGroup>(), 1f, timeToShow);
        LeanTween.scale(MessageBox, Vector3.one, timeToShow).setOnComplete(() =>{
            playerInput.currentActionMap.FindAction("Click").performed += HideMessageBox;
        });
    }

    public void HideMessageBox(InputAction.CallbackContext callbackContext)
    {
        playerInput.currentActionMap.FindAction("Click").performed -= HideMessageBox;
        _ResetCodeLabel();

        float timeToHide = 0.25f;

        LeanTween.alphaCanvas(MessageBox.GetComponent<CanvasGroup>(), 0f, timeToHide);
        LeanTween.scale(MessageBox, Vector3.zero, timeToHide).setOnComplete(() => { MessageBox.SetActive(false); }); 
    }

    private void ClosePhoneEntry()
    {
        float timeToClose = 0.25f;
        LeanTween.alphaCanvas(canvasGroup, 0, timeToClose);
        LeanTween.scale(gameObject, new Vector3(3,3,3), timeToClose).setOnComplete(() => {
            PhoneGUI.Instance.OpenMainScreen();
        });
    }
}
