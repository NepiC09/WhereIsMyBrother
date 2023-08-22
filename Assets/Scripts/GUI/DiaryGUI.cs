using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class DiaryGUI : MonoBehaviour, IDataPersistance
{
    public static DiaryGUI Instance { get; private set; }

    [SerializeField] private GameObject firstPlace;
    [SerializeField] private GameObject[] twoFifthPlaces;
    [SerializeField] private GameObject lastPlace;

    [SerializeField] private GameObject readButtons;

    [SerializeField] private PlayerInput playerInput;

    [SerializeField] private TextAsset somebodyOrderStuffs;
    [SerializeField] private TextAsset badEquip;
    [SerializeField] private TextAsset andrewInCountry;
    [SerializeField] private TextAsset grandmaNumber;
        
    public int counter = 0;
    public bool[] phrasedReaded = new bool[6];
    public int[] indexPhrase = new int[6];

    /*
       "FirstUpdate"-----------1
       "SomebodyOrderStuffs"---2
       "BadEquip"--------------3
       "AndrewInCountry"-------4
       "GrandmaNumber"---------5
       "AndrewArested"---------6
    */

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        playerInput.currentActionMap.FindAction("OpenDiary").performed += OpenDiaryPerformed;
        gameObject.SetActive(false);
        readButtons.SetActive(false);
    }

    public void LoadData(GameData gameData)
    {
        counter = gameData.diariGUI_counter;
        indexPhrase = gameData.diariGUI_indexPhrase;
        phrasedReaded = gameData.diariGUI_phrasedReaded;

        for (int i = 0; i < 6; i++)
        {
            string message = "";
            switch (indexPhrase[i])
            {
                case 1:
                    message = Diary.ru_RU_MessageFirstUpdate;
                    break;
                case 2:
                    message = Diary.ru_RU_MessageSomebodyOrderStuffs;
                    break;
                case 3:
                    message = Diary.ru_RU_MessageBadEquip;
                    break;
                case 4:
                    message = Diary.ru_RU_MessageAndrewInCountry;
                    break;
                case 5:
                    message = Diary.ru_RU_MessageGrandmaNumber;
                    break;
                case 6:
                    message = Diary.ru_RU_MessageAndrewArested;
                    break;
            }
            if (i == 0)
            {
                firstPlace.GetComponentInChildren<TextMeshProUGUI>().text = message;
            } 
            else if (i> 0 && i < 5)
            {
                twoFifthPlaces[i-1].GetComponentInChildren<TextMeshProUGUI>().text = message;
            }
            else
            {
                lastPlace.GetComponentInChildren<TextMeshProUGUI>().text = message;
            }
        }
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.diariGUI_counter = counter;
        gameData.diariGUI_indexPhrase = indexPhrase;
        gameData.diariGUI_phrasedReaded = phrasedReaded;
    }

    private void OpenDiaryPerformed(InputAction.CallbackContext o)
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        if (!isActiveAndEnabled)
        {
            Show();
            if (counter == 5)
            {
                OpenButtons();
            }
        }
        else
        {
            Hide();
        }
    }

    private void Show()
    {
        PlayerController.Instance.setPlayable(false);
        transform.localScale = Vector3.zero;
        gameObject.SetActive(true);
        gameObject.LeanScale(Vector3.one, 0.1f).setOnComplete(() => { PlayerController.Instance.setPlayable(false); });
        playerInput.currentActionMap.FindAction("Cancel").performed += CancelPerformed;
    }

    private void Hide()
    {
        PlayerController.Instance.setPlayable(true);
        gameObject.LeanScale(Vector3.zero, 0.1f).setOnComplete(() => { gameObject.SetActive(false); });
    }

    private void CancelPerformed(InputAction.CallbackContext o)
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        PlayerController.Instance.setPlayable(true);
        gameObject.LeanScale(Vector3.zero, 0.1f).setOnComplete(() => { gameObject.SetActive(false); });
    }

    public void SetPlace(string text, int phraseIndex)
    {
        if (counter == 0)
        {
            firstPlace.GetComponentInChildren<TextMeshProUGUI>().text = text;
            counter++;
            indexPhrase[0] = 1;
        } 
        else if(counter > 0 && counter < 5)
        {
            twoFifthPlaces[counter -1].GetComponentInChildren<TextMeshProUGUI>().text = text;
            indexPhrase[counter] = phraseIndex;
            counter++;
        }
        else
        {
            lastPlace.GetComponentInChildren<TextMeshProUGUI>().text = text;
            indexPhrase[5] = 6;
            counter++;
        }

        if (counter == 5)
        {
            OpenButtons();
        }
    }

    public void _ReadPlace(int indexPlace)
    {
        if(DialogueManager.Instance.isDialoguePlaying)
            return;

        switch (indexPhrase[indexPlace-1])
        {
            case 1:
                break;
            case 2:
                DialogueManager.Instance._StartDialogue(somebodyOrderStuffs);
                DialogueManager.Instance.ContinueStory();
                break;
            case 3:
                DialogueManager.Instance._StartDialogue(badEquip);
                DialogueManager.Instance.ContinueStory();
                break;
            case 4:
                DialogueManager.Instance._StartDialogue(andrewInCountry);
                DialogueManager.Instance.ContinueStory();
                break;
            case 5:
                DialogueManager.Instance._StartDialogue(grandmaNumber);
                DialogueManager.Instance.ContinueStory();
                break;
            case 6:
                break;
        }

        phrasedReaded[indexPlace - 1] = true;

        StartCoroutine(SetLastPhrase());
    }

    private IEnumerator SetLastPhrase()
    {
        if (phrasedReaded[1] && phrasedReaded[2] && phrasedReaded[3] && phrasedReaded[4])
        {
            while (DialogueManager.Instance.isDialoguePlaying)
            {
                yield return null;
            }
            GreenSegment.SetValue("isGreenStarted", true);
            Diary.SetValue("AndrewArested", true);
        }
    }

    private void OpenButtons()
    {
        readButtons.SetActive(true);
        foreach (Transform child in readButtons.GetComponentInChildren<Transform>())
        {
            child.localScale = Vector3.zero;
            child.gameObject.SetActive(true);
            child.LeanScale(Vector3.one, 0.1f);
        }
    }

    public void _CloseButton(GameObject button)
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        button.LeanScale(Vector3.zero, 0.1f).setOnComplete(() => { button.SetActive(false); });
    }
}
