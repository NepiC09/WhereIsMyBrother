using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistance
{
    public static GameManager Instance { get; private set; }
    public const string RU_LOCALIZATION = "ru_RU";
    private string language;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        language = RU_LOCALIZATION;
    }
    public void LoadData(GameData gameData)
    {
        language = gameData.gameManager_language;
    }
    public void SaveData(ref GameData gameData)
    {
        gameData.gameManager_language = language;
    }

    public string GetLanguage()
    {
        return language;
    }
}
