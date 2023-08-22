using UnityEngine;
using UnityEngine.InputSystem;

public class PhoneGUI : MonoBehaviour, IDataPersistance
{
    public static PhoneGUI Instance { get; private set; }

    [SerializeField] private PhoneEntry phoneEntry;
    [SerializeField] private MainScreen mainScreen;
    [SerializeField] private InternetScreen internetScreen;
    [SerializeField] private GalleryScreen galleryScreen;
    [SerializeField] private CallScreen callScreen;
    [SerializeField] private SMSScreen smsScreen;
    [SerializeField] private MessangerScreen messangerScreen;

    [SerializeField] private PlayerInput playerInput;

    public bool isInternetChecked = false;
    public bool isGalleryChecked = false;

    public bool isSMSChecked = false;
    public bool isMessangerChecked = false;

    private void Awake()
    {
        Instance = this;
    }
    public void LoadData(GameData gameData)
    {
        isInternetChecked = gameData.phoneGUI_isInternetChecked;
        isGalleryChecked = gameData.phoneGUI_isGalleryChecked;
        isSMSChecked = gameData.phoneGUI_isSMSChecked;
        isMessangerChecked = gameData.phoneGUI_isMessangerChecked;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.phoneGUI_isInternetChecked = isInternetChecked;
        gameData.phoneGUI_isGalleryChecked = isGalleryChecked;
        gameData.phoneGUI_isSMSChecked = isSMSChecked;
        gameData.phoneGUI_isMessangerChecked = isMessangerChecked;
    }

    public void OpenPhone()
    {
        playerInput.currentActionMap.FindAction("Cancel").performed += CancelPerformed;

        gameObject.transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        gameObject.LeanScale(Vector3.one, 0.1f);

        if (!phoneEntry.isFisrtOpenedMainScreen)
        {
            phoneEntry.OpenEntryScreen();
            mainScreen.gameObject.SetActive(false);
        }
        else
        {
            phoneEntry.gameObject.SetActive(false);
            mainScreen.gameObject.SetActive(true);
        }
        internetScreen.gameObject.SetActive(false);
        galleryScreen.gameObject.SetActive(false);
        callScreen.gameObject.SetActive(false);
        smsScreen.gameObject.SetActive(false);
        messangerScreen.gameObject.SetActive(false);
    }

    private void ClosePhone()
    {
        playerInput.currentActionMap.FindAction("Cancel").performed -= CancelPerformed;

        gameObject.LeanScale(Vector3.zero, 0.1f).setOnComplete(() => { gameObject.SetActive(false); });

        phoneEntry.gameObject.SetActive(false);
        mainScreen.gameObject.SetActive(false);
        internetScreen.gameObject.SetActive(false);
        galleryScreen.gameObject.SetActive(false);
        callScreen.gameObject.SetActive(false);
        smsScreen.gameObject.SetActive(false);
        messangerScreen.gameObject.SetActive(false);
    }

    private void CancelPerformed(InputAction.CallbackContext obj)
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        ClosePhone();
    }

    public void OpenMainScreen()
    {
        phoneEntry.gameObject.SetActive(false); 
        mainScreen.gameObject.SetActive(true);
        internetScreen.gameObject.SetActive(false);
        galleryScreen.gameObject.SetActive(false);
        callScreen.gameObject.SetActive(false);
        smsScreen.gameObject.SetActive(false);
        messangerScreen.gameObject.SetActive(false);
    }

    public void OpenInternetScreen()
    {
        internetScreen.transform.localPosition = new Vector3(-500, 0);
        internetScreen.OpenInternetScreen();
        internetScreen.transform.LeanMoveLocalX(0, 0.1f).setOnComplete(() =>{
            phoneEntry.gameObject.SetActive(false);
            mainScreen.gameObject.SetActive(false);
            galleryScreen.gameObject.SetActive(false);
            callScreen.gameObject.SetActive(false);
            smsScreen.gameObject.SetActive(false);
            messangerScreen.gameObject.SetActive(false);
        });
    }

    public void OpenGalleryScreen() {
        galleryScreen.transform.localPosition = new Vector3(-500, 0);
        galleryScreen.OpenGalleryScreen();
        galleryScreen.transform.LeanMoveLocalX(0, 0.1f).setOnComplete(() => {
            phoneEntry.gameObject.SetActive(false);
            mainScreen.gameObject.SetActive(false);
            internetScreen.gameObject.SetActive(false);
            callScreen.gameObject.SetActive(false);
            smsScreen.gameObject.SetActive(false);
            messangerScreen.gameObject.SetActive(false);
        });
    }

    public void OpenCallScreen()
    {
        callScreen.transform.localPosition = new Vector3(0, -1000);
        callScreen.OpenCallScreen();
        callScreen.transform.LeanMoveLocalY(0, 0.1f).setOnComplete(() => {
            phoneEntry.gameObject.SetActive(false);
            mainScreen.gameObject.SetActive(false);
            internetScreen.gameObject.SetActive(false);
            galleryScreen.gameObject.SetActive(false);
            smsScreen.gameObject.SetActive(false);
            messangerScreen.gameObject.SetActive(false);
        });
    }

    public void OpenSMSScreen()
    {
        smsScreen.transform.localPosition = new Vector3(0, -1000);
        smsScreen.OpenSMSScreen();
        smsScreen.transform.LeanMoveLocalY(0, 0.1f).setOnComplete(() => {
            phoneEntry.gameObject.SetActive(false);
            mainScreen.gameObject.SetActive(false);
            internetScreen.gameObject.SetActive(false);
            galleryScreen.gameObject.SetActive(false);
            callScreen.gameObject.SetActive(false);
            messangerScreen.gameObject.SetActive(false);
        });
    }

    public void OpenMessangerScreen()
    {
        messangerScreen.transform.LeanMoveLocalX(-500, 0f);
        messangerScreen.OpenMessangerScreen();
        messangerScreen.transform.LeanMoveLocalX(0, 0.1f).setOnComplete(() => {
            phoneEntry.gameObject.SetActive(false);
            mainScreen.gameObject.SetActive(false);
            galleryScreen.gameObject.SetActive(false);
            callScreen.gameObject.SetActive(false);
            smsScreen.gameObject.SetActive(false);
            internetScreen.gameObject.SetActive(false);
        });
    }
}
