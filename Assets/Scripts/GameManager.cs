using UnityEngine;

public class GameManager : MonoBehaviour, IDataPersistance
{
    public static GameManager Instance { get; private set; }

    [SerializeField] public CompositeCollider2D hallway;
    [SerializeField] public CompositeCollider2D entrance;
    [SerializeField] public CompositeCollider2D bathroom;
    [SerializeField] public CompositeCollider2D livingroom;
    [SerializeField] public CompositeCollider2D kitchen;
    [SerializeField] public CompositeCollider2D annroom;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Fader.Instance.FadeOut(1f);
    }
    public void LoadData(GameData gameData)
    {
        //GlobalScripts.language = gameData.gameManager_language;
    }
    public void SaveData(ref GameData gameData)
    {
        //gameData.gameManager_language = GlobalScripts.language;
    }

    /*
    public string GetLanguage()
    {
        return GlobalScripts.language;
    }
    */
}
