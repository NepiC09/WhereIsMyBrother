using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveSystemManager : MonoBehaviour
{
    //[Header("File Storage Config")]
    public string fileName { get; private set; }

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
        fileName = GlobalScripts.currentSaveFileName;

        fileDataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
        this.dataPersistanceObjectsList = FindAllDataPersistanceObjects();

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            if (GlobalScripts.isStartedNewGame)
            {
                NewGame();
                if (fileName == "")
                    Debug.LogError("Игра загружает несуществующий сейв");
            }
            else
            {
                LoadGame(fileName);
                if (fileName == "")
                    Debug.LogError("Игра загружает несуществующий сейв");
            }
        }
        else
        {
            this.gameData = fileDataHandler.Load(fileName);
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

    public void LoadGame(string fileName)
    {
        this.gameData = fileDataHandler.Load(fileName);

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

    public void SaveGame(string fileName)
    {

        MessageBoxes.Instance.OpenUpLeftMessage("Игра сохраняется");

        foreach (IDataPersistance dataPersistance in dataPersistanceObjectsList)
        {
            dataPersistance.SaveData(ref gameData);
        }

        fileDataHandler.Save(gameData, fileName);
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

    public bool isHaveSave(string fileName)
    {
        return fileDataHandler.isExistSaveFile(fileName);
    }

    public DateTime returnLastWrite(string fileName)
    {
        return fileDataHandler.returnLastWriteFile(fileName);
    }
}
