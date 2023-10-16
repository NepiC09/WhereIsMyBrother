using System.Collections;
using System.Collections.Generic;
using Ink.Runtime;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class DialogueManager : MonoBehaviour, IDataPersistance
{
    public static DialogueManager Instance { get; private set; }

    [Header("Params")]
    [SerializeField] private float typingSpeed = 0.04f;

    [Header("Global Ink JSON")]
    [SerializeField] private TextAsset loadGlobalsJSON;

    [Header("Dialogue GUI")]
    [SerializeField] private GameObject dialogueGUI;
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TextMeshProUGUI speakerLabel;
    [SerializeField] private TextMeshProUGUI dialogueLabel;
    [SerializeField] private WordChoices wordChoices;

    [Header("Choices GUI")]
    [SerializeField] private GameObject[] choiceArray;
    private TextMeshProUGUI[] choiceArrayText;

    private Story currentStory;
    public bool isDialoguePlaying { get; private set; }
    private bool canContinueToNextLine = false;

    private const string SPEAKER_TAG = "speaker";
    private const string TYPE_ANSWER_TAG = "type_answer";
    private string typeAnswer = "default";

    private Coroutine displayLineCoroutine;

    private DialogueVariables dialogueVariables;

    private void Awake()
    {
        Instance = this;
        dialogueVariables = new DialogueVariables(loadGlobalsJSON);
    }

    private void Start()
    {
        isDialoguePlaying = false;
        HideAll();

        //get choices texts
        choiceArrayText = new TextMeshProUGUI[choiceArray.Length];
        int i = 0;
        foreach(GameObject choice in choiceArray) 
        {
            choiceArrayText[i] = choice.GetComponentInChildren<TextMeshProUGUI>();
            i++;
        }
    }

    public void LoadData(GameData gameData)
    {
        dialogueVariables.SetVariables(gameData.dialogueGlobalVariables);
    }

    public void SaveData(ref GameData gameData)
    {
        gameData.dialogueGlobalVariables.Clear();
        foreach (string key in dialogueVariables.variables.Keys)
        {
            bool boolValue = false;
            if (dialogueVariables.variables[key].ToString() == "1")
            {
                boolValue = true;
            }
            else if (dialogueVariables.variables[key].ToString() == "0")
            {
                boolValue = false;
            }
            gameData.dialogueGlobalVariables.Add(key, boolValue);
        }
    }

    public void _NextPhrase(InputAction.CallbackContext context)
    {
        if (context.ReadValueAsButton())
            return;

        if (isDialoguePlaying && currentStory.currentChoices.Count == 0)
        {
            if (canContinueToNextLine)
            {
                ContinueStory();
            }
            else
            {
                if (displayLineCoroutine != null)
                {
                    StopCoroutine(displayLineCoroutine); displayLineCoroutine = null;
                }
                dialogueLabel.text = currentStory.currentText;
                canContinueToNextLine = true;
            }
        }
    }

    public void _StartDialogue(TextAsset inkJSON)
    {
        PlayerController.Instance.setPlayable(false);
        currentStory = new Story(inkJSON.text);
        currentStory.ChoosePathString(LanguageManager.Instance.language);
        isDialoguePlaying = true;
        dialogueGUI.SetActive(true);
        dialoguePanel.SetActive(true);
        canContinueToNextLine = true;

        dialogueVariables.StartListening(currentStory);
        //ContinueStory();
    }

    private IEnumerator ExitDialogueMode()
    {
        yield return new WaitForSeconds(0.1f);

        dialogueVariables.StopListening(currentStory);
        isDialoguePlaying = false;
        HideAll();
        dialogueLabel.text = "";
        speakerLabel.text = "";

        PlayerController.Instance.setPlayable(true);
    }

    private void HideAll()
    {
        dialogueGUI.SetActive(false);
        dialoguePanel.SetActive(false);
        wordChoices.gameObject.SetActive(false);
    }

    public void ContinueStory()
    {
        if (currentStory.canContinue)
        {
            if(displayLineCoroutine != null)
            {
                StopCoroutine(displayLineCoroutine);
            }

            displayLineCoroutine = StartCoroutine(DisplayLine(currentStory.Continue()));
            if (currentStory.currentText.Length == 0)
            {
                ContinueStory();
                return;
            }
            HandleTags(currentStory.currentTags);
            StartCoroutine(GetAnswer(typeAnswer));
            typeAnswer = "default";

            if (currentStory.currentText.Length == 0)
            {
                ContinueStory();
                return;
            }
            //if(currentStory.currentChoices.Count > 0 && (!choiceArray[0].gameObject.activeInHierarchy && !wordChoices.gameObject.activeInHierarchy))
            //{
            //    DisplayChoices();
            //}
        }
        else
        {
            StartCoroutine(ExitDialogueMode());
        }
    }

    private IEnumerator DisplayLine(string line)
    {
        canContinueToNextLine = false;

        dialogueLabel.text = "";
        foreach(char letter in line.ToCharArray())
        {
            dialogueLabel.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        canContinueToNextLine = true;
    }

    private bool returnCan()
    {
        return canContinueToNextLine;
    }

    private void HandleTags(List<string> currentTags)
    {
        foreach (string tag in currentTags)
        {
            string[] splitTag = tag.Split(':');
            if(splitTag.Length != 2)
            {
                Debug.LogError("Tag could not be approriately parsed: " + tag);
            }
            string tagKey = splitTag[0].Trim();
            string tagValue = splitTag[1].Trim();

            switch(tagKey)
            {
                case SPEAKER_TAG:
                    speakerLabel.text = tagValue;
                    break;
                case TYPE_ANSWER_TAG:
                    typeAnswer = tagValue;
                    //StartCoroutine(GetAnswer(tagValue));
                    break;
                default:
                    Debug.LogWarning("This tag is not currently handled: " + tag);
                    break;
            }
        }
    }

    private IEnumerator GetAnswer(string type)
    {
        yield return new WaitUntil(returnCan);

        const string DEFAULT_ANSWER = "default";
        const string UNIQUE_ANSWER = "unique";
        
        switch(type)
        {
            case DEFAULT_ANSWER:
                DisplayChoices();
                break;
            case UNIQUE_ANSWER:
                wordChoices.SetWordChoices(currentStory);
                break;
            default:
                Debug.LogError("There's not this type of answer: " + type);
                break;
        }
    }

    private void DisplayChoices()
    { 
        List<Choice> currentChoices = currentStory.currentChoices;

        if (currentChoices.Count > choiceArray.Length)
        {
            Debug.LogError("More choices were given than the GUI can show:" + currentChoices.Count);
        }

        int i = 0;
        foreach (Choice choice in currentChoices)
        {
            choiceArray[i].gameObject.SetActive(true);
            choiceArrayText[i].text = choice.text;
            i++;
        }
        for(int index = i; index < choiceArray.Length; index++)
        {
            choiceArray[index].gameObject.SetActive(false);
        }

        StartCoroutine(SelectFirstChoice());
    }

    private IEnumerator SelectFirstChoice()
    {
        EventSystem.current.SetSelectedGameObject(null);
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(choiceArray[0].gameObject);
    }

    public void MakeChoice(int choiceIndex)
    {
        if (canContinueToNextLine)
        {
            currentStory.ChooseChoiceIndex(choiceIndex);
            ContinueStory();

            DisplayChoices();
        }
    }

    public Ink.Runtime.Object GetVariableState(string variableName)
    {
        Ink.Runtime.Object variableValue = null;
        dialogueVariables.variables.TryGetValue(variableName, out variableValue);
        if(variableName == null)
        {
            Debug.LogWarning("Ink variable was found to be null: " + variableName);
        }
        return variableValue;
    }

    public void SetVariableState(string variableName, Ink.Runtime.Object value)
    {
        if (dialogueVariables.variables.ContainsKey(variableName))
        {
            dialogueVariables.variables.Remove(variableName);
            dialogueVariables.variables.Add(variableName, value);
        }
    }
}
