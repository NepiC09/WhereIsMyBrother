using UnityEngine;
using UnityEngine.Events;

public class LanguageManager : MonoBehaviour
{
    public static LanguageManager Instance { get; private set; }

    public const string RU_LOCALIZATION = "ru_RU";
    public const string EN_LOCALIZATION = "en_EN";
    public string language {  get; private set; }
    public UnityEvent<string> languageChanged;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        language = GlobalScripts.language;
        Debug.Log(language);
        //languageChanged?.Invoke(language);
    }

    public void _onLanguageButtonPressed(string lang)
    {
        if (lang == RU_LOCALIZATION)
        {
            language = RU_LOCALIZATION;
            GlobalScripts.language = language;
            languageChanged?.Invoke(language);
        }
        else if (lang == EN_LOCALIZATION)
        {
            language = EN_LOCALIZATION;
            GlobalScripts.language = language;
            languageChanged?.Invoke(language);
        }
        else
        { 
            Debug.LogWarning("language isn't exist in game: " + lang);
        }
    }
}
