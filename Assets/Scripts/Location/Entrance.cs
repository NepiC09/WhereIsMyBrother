using UnityEngine;

public class Entrance : MonoBehaviour
{

    [SerializeField] private LittleBoy littleBoy;
    [SerializeField] private GameObject dayBackground;
    [SerializeField] private GameObject eveningBackground;

    [SerializeField] private GameObject[] russianText;
    [SerializeField] private GameObject[] englishText;

    private void Start()
    {
        LanguageManager.Instance.languageChanged.AddListener(_onLanguageChanged);
    }

    private void _onLanguageChanged(string language)
    {
        if(language == LanguageManager.RU_LOCALIZATION)
        {
            foreach(GameObject text in russianText)
            {
                text.SetActive(true);
            }
            foreach(GameObject text in englishText)
            {
                text.SetActive(false);
            }
        } else if (language == LanguageManager.EN_LOCALIZATION)
        {
            foreach(GameObject text in russianText)
            {
                text.SetActive(false);
            }
            foreach (GameObject text in englishText)
            {
                text.SetActive(true);
            }
        }
    }

    public void _SetDayOrEvening()
    {
        if (GreenSegment.GetValue("isSlept"))
        {
            dayBackground.SetActive(true);
            eveningBackground.SetActive(false);
            littleBoy.transform.parent.gameObject.SetActive(false);
        }
        else
        {
            dayBackground.SetActive(false);
            eveningBackground.SetActive(true);
            littleBoy.transform.parent.gameObject.SetActive(true);
        }
    }
}
