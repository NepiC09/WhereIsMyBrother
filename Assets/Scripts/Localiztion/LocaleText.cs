using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LocaleText : MonoBehaviour
{
    [Header("Localed text")]
    [SerializeField] private string ru_Text;
    [SerializeField] private string en_Text;
    
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
        if(text == null)
        {
            Debug.LogWarning("Text label isn't TextMeshPro: " + gameObject);
        }
    }

    void Start()
    {     
        StartCoroutine(WaitChangeLanguage());
    }

    private IEnumerator WaitChangeLanguage() {
        yield return new WaitForSeconds(1f);

        LanguageManager.Instance.languageChanged.AddListener(localeChanged);
    }


    private void localeChanged(string locale)
    {
        if (locale == LanguageManager.RU_LOCALIZATION)
        {
            text.text = ru_Text;
        }
        else if (locale == LanguageManager.EN_LOCALIZATION)
        {
            text.text = en_Text;
        }
        else
        {
            Debug.LogWarning("language isn't exist in game: " + locale);
        }
    }
}
