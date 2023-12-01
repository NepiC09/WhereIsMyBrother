using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Keyboard : MonoBehaviour
{
    public static Keyboard Instance { get; private set; }
    [SerializeField] private GameObject ru_RU_Keyboard;
    [SerializeField] private GameObject en_EN_Keyboard;

    private RectTransform rectTransform;

    public List<Button> buttonArray;

    public UnityEvent<string> onLetterPressed;
    public UnityEvent<string> onSpacePressed;
    public UnityEvent onSearchPressed;
    public UnityEvent onDeletePressed;

    public bool isListened = false;

    private void Awake()
    {
        Instance = this;
        buttonArray = new List<Button>();
        rectTransform = GetComponent<RectTransform>();
    }

    public void openKeyboard()
    {
        if (LanguageManager.Instance.language == LanguageManager.RU_LOCALIZATION)
        {
            ru_RU_Keyboard.SetActive(true);
            en_EN_Keyboard.SetActive(false);
        }
        else if (LanguageManager.Instance.language == LanguageManager.EN_LOCALIZATION)
        {
            ru_RU_Keyboard.SetActive(false);
            en_EN_Keyboard.SetActive(true);
        }
        gameObject.SetActive(true);
        rectTransform.LeanMoveY(0f, 0.2f);
        //LeanTween.moveLocalY(this.gameObject, 0, 0.2f);
    }

    public void closeKeyboard()
    {
        if (rectTransform.localPosition.y != -500)
        {
            rectTransform.LeanMoveY(-500f, 0.2f).setOnComplete(() => { gameObject.SetActive(false); });
            //rectTransform.LeanMoveLocalY(-500f, 0.2f).setOnComplete(() => { gameObject.SetActive(false); });
        }
    }

    public void onKeyPressed(string buttonText)
    {
        switch (buttonText)
        {
            case "^":
                SetUppercase();
                break;
            case "<=":
                onDeletePressed?.Invoke();
                break;
            case "":
                onSpacePressed?.Invoke(" ");
                break;
            case "->":
                onSearchPressed?.Invoke();
                break;
            default:
                onLetterPressed?.Invoke(buttonText);
                break;
        }
    }

    private void SetUppercase()
    {
        foreach (Button button in buttonArray)
        {
            TMP_Text buttonText = button.GetComponentInChildren<TMP_Text>();

            if(buttonText.fontStyle== FontStyles.UpperCase)
            {
                buttonText.fontStyle = FontStyles.LowerCase;
                buttonText.text = buttonText.text.ToLower();
            } 
            else if (buttonText.fontStyle == FontStyles.LowerCase)
            {
                buttonText.fontStyle = FontStyles.UpperCase;
                buttonText.text = buttonText.text.ToUpper();
            }
        }
    }
}
