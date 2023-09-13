using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystemManager : MonoBehaviour
{
    [Header("File Storage Config")]
    [SerializeField] private string fileName;

    private GameData gameData;
    private List<IDataPersistance> dataPersistanceObjectsList;
    private FileDataHandler fileDataHandler;

    public static SaveSystemManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistanceObjectsList = FindAllDataPersistanceObjects();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (GlobalScripts.isStartedNewGame)
            {
                NewGame();
            }
            else
            {
                LoadGame();
            }
        }
        else
        {
            this.gameData = fileDataHandler.Load();
        }
    }

    public void NewGame()
    {
        MessageBoxes.Instance.OpenUpLeftMessage("Начата новая игра");
        this.gameData = new GameData();
        foreach (IDataPersistance dataPersistance in dataPersistanceObjectsList)
        {
            dataPersistance.LoadData(gameData);
        }
        //SaveGame();
    }

    public void LoadGame()
    {
        this.gameData = fileDataHandler.Load();

        MessageBoxes.Instance.OpenUpLeftMessage("Игра загружается");
        if (this.gameData == null)
        {
            NewGame();
        }

        foreach(IDataPersistance dataPersistance in dataPersistanceObjectsList)
        {
            dataPersistance.LoadData(gameData);
        }
    }

    public void SaveGame()
    {

        MessageBoxes.Instance.OpenUpLeftMessage("Игра сохраняется");

        foreach (IDataPersistance dataPersistance in dataPersistanceObjectsList)
        {
            dataPersistance.SaveData(ref gameData);
        }

        fileDataHandler.Save(gameData);
    }

    private void OnApplicationQuit()
    {
        //SaveGame();
    }

    private List<IDataPersistance> FindAllDataPersistanceObjects()
    {
        IEnumerable<IDataPersistance> dataPersistanceObjects = FindObjectsOfType<MonoBehaviour>(true).OfType<IDataPersistance>();

        return new List<IDataPersistance>(dataPersistanceObjects);
    }

    public bool isHaveSave()
    {
        return fileDataHandler.Load() != null;
    }
}
