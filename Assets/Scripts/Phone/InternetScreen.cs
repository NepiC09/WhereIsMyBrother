using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InternetScreen : MonoBehaviour
{
    [SerializeField] private GameObject site1Screen;
    [SerializeField] private GameObject site2Screen;
    [SerializeField] private GameObject recruitScreen;
    [SerializeField] private GameObject browserScreen;

    [SerializeField] private Button leftButton;
    [SerializeField] private Button rightButton;

    [SerializeField] private TextMeshProUGUI searchLabel;
    [SerializeField] private TextAsset wrongSearch;

    enum States
    {
        FirstScreen, RecruitScreen, KeyBoardOnFirstScreen, BrowserScreen, Site1, Site2
    }
    private States state = States.FirstScreen;
    private States prevState;

    public void OpenInternetScreen()
    {
        state = States.FirstScreen;
        prevState = state;
        //Opening only first screen
        gameObject.SetActive(true);
        recruitScreen.SetActive(false); 
        site1Screen.SetActive(false); 
        site2Screen.SetActive(false); 
        browserScreen.SetActive(false);
        Keyboard.Instance.closeKeyboard();
    }

    public void _LeftButtonPressed()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        switch (state)
        {
            case States.FirstScreen:
                MoveToMainScreen();
                break;
            case States.RecruitScreen:
                recruitScreen.transform.LeanScale(Vector3.zero, 0.1f).setOnComplete(() => { recruitScreen.gameObject.SetActive(false);});
                state = prevState;
                break;
            case States.Site1:
                site1Screen.transform.LeanScale(Vector3.zero, 0.1f).setOnComplete(() => { site1Screen.gameObject.SetActive(false);});
                state = prevState;
                break;
            case States.Site2:
                site2Screen.transform.LeanScale(Vector3.zero, 0.1f).setOnComplete(() => { site2Screen.gameObject.SetActive(false);});
                state = prevState;
                break;
            case States.KeyBoardOnFirstScreen:
                CloseKeyboard();
                state = prevState;
                break;
            case States.BrowserScreen:
                browserScreen.LeanScale(Vector3.zero, 0.2f).setOnComplete(() => {
                    browserScreen.gameObject.SetActive(false);
                });
                prevState = state;
                state = States.FirstScreen;
                break;
        }
    }

    public void _RightButtonPressed()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        switch (state)
        {
            case States.FirstScreen:
                MoveToMainScreen();
                break;
            case States.RecruitScreen:
                MoveToMainScreen();
                break;
            case States.Site1:
                MoveToMainScreen();
                break;
            case States.Site2:
                MoveToMainScreen();
                break;
            case States.KeyBoardOnFirstScreen:
                CloseKeyboard();
                MoveToMainScreen();
                break;
            case States.BrowserScreen:
                CloseKeyboard();
                MoveToMainScreen();
                break;
        }
    }

    private void MoveToMainScreen()
    {
        PhoneGUI.Instance.OpenMainScreen();
        gameObject.SetActive(true);

        transform.LeanMoveLocalX(-500, 0.1f).setOnComplete(() => {
            gameObject.SetActive(false);
        });
    }

    public void _OpenRecruitScreen() {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        if (state == States.KeyBoardOnFirstScreen)
        {
            CloseKeyboard();
            state = prevState;
        }

        prevState = state;
        state = States.RecruitScreen;
        recruitScreen.transform.localScale = Vector3.zero;
        recruitScreen.LeanScale(Vector3.one, 0.1f);
        recruitScreen.SetActive(true);
    }

    public void _OpenSite1Screen()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        if (state == States.KeyBoardOnFirstScreen)
        {
            CloseKeyboard();
            state = prevState;
        }

        prevState = state;
        state = States.Site1;
        site1Screen.transform.localScale = Vector3.zero;
        site1Screen.LeanScale(Vector3.one, 0.1f);
        site1Screen.SetActive(true);
    }

    public void _OpenSite2Screen()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;

        if (state == States.KeyBoardOnFirstScreen)
        {
            CloseKeyboard();
            state = prevState;
        }

        prevState = state;
        state = States.Site2;
        site2Screen.transform.localScale = Vector3.zero;
        site2Screen.LeanScale(Vector3.one, 0.1f);
        site2Screen.SetActive(true);


        //Diary stuff
        if (PhoneGUI.Instance.isInternetChecked)
            return;

        PhoneGUI.Instance.isInternetChecked = true;
        if (PhoneGUI.Instance.isGalleryChecked)
        {
            Diary.SetValue("BadEquip", true);
        }
    }

    private void CloseKeyboard()
    {
        leftButton.GetComponentInChildren<Transform>().LeanRotateZ(0, 0.25f);
        Keyboard.Instance.closeKeyboard();
        searchLabel.text = searchLabel.text.Replace("|", string.Empty);
        if (searchLabel.text.Length == 0)
        {
            if (LanguageManager.Instance.language == LanguageManager.RU_LOCALIZATION)
            {
                searchLabel.text = "¬ведите поисковой запрос или URL-...";
            } 
            else if (LanguageManager.Instance.language == LanguageManager.EN_LOCALIZATION)
            {
                searchLabel.text = "Search text or URL...";
            }
        }
    }

    public void _OpenKeyboard()
    {
        if (DialogueManager.Instance.isDialoguePlaying)
            return;
        if (state == States.KeyBoardOnFirstScreen)
            return;

        prevState = state;
        state = States.KeyBoardOnFirstScreen;

        if(searchLabel.text == "¬ведите поисковой запрос или URL-..." || searchLabel.text == "Search text or URL...")
        {
            searchLabel.text = "";
        }
        leftButton.GetComponentInChildren<Transform>().LeanRotateZ(90, 0.25f);

        Keyboard.Instance.openKeyboard();
        StartCoroutine(LastLetter());

        if (Keyboard.Instance.isListened == false)
        {
            Keyboard.Instance.isListened = true;
            ListenKeyboard();
        }
    }

    private void ListenKeyboard()
    {
        Keyboard.Instance.onLetterPressed.AddListener((string letter) =>
        {
            searchLabel.text += letter;
        });
        Keyboard.Instance.onSpacePressed.AddListener((string letter) =>
        {
            searchLabel.text += letter;
        });
        Keyboard.Instance.onDeletePressed.AddListener(() =>
        {
            searchLabel.text = searchLabel.text.Replace("|", string.Empty);
            if (searchLabel.text.Length > 0)
            {
                searchLabel.text = searchLabel.text.Remove(searchLabel.text.Length - 1, 1);
            }
        });
        Keyboard.Instance.onSearchPressed.AddListener(() =>
        {
            searchLabel.text = searchLabel.text.Replace("|", string.Empty);
            SearchCompare();
            CloseKeyboard();
        });
    }

    private IEnumerator LastLetter()
    {
        while (state == States.KeyBoardOnFirstScreen)
        {
            yield return new WaitForSeconds(0.5f);
            searchLabel.text += "|";

            float time = 0.5f;
            while (time > 0f)
            {
                yield return null;
                if (searchLabel.text.Length > 1)
                {
                    if (searchLabel.text[searchLabel.text.Length - 1] != '|')
                    {
                        searchLabel.text = searchLabel.text.Replace("|", string.Empty);
                        searchLabel.text += "|";
                    }
                }
                time -= Time.deltaTime;
            }
            searchLabel.text = searchLabel.text.Replace("|", string.Empty);
        }
    }

    private void SearchCompare()
    {
        /////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////// —ƒ≈Ћј“№ Ћќ јЋ»«ј÷»ё
        //только если вз€т нож
        if((searchLabel.text.ToLower() == "дф колосс" || searchLabel.text.ToLower() == "vf colossus") && BlueSegment.GetValue("isKnifeGotten"))
        {
            prevState = state;
            state = States.BrowserScreen;
            browserScreen.gameObject.SetActive(true);
            browserScreen.LeanScale(Vector3.one, 0.2f);
        }
        else
        {
            if (searchLabel.text.Length > 0)
            {
                DialogueManager.Instance._StartDialogue(wrongSearch);
                DialogueManager.Instance.ContinueStory();
            }
            //searchLabel.text = searchLabel.text.Replace("|", string.Empty);
            prevState = state;
            state = States.FirstScreen;
        }
    }
}
