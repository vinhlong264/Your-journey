using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    [SerializeField] private GameData gameData;
    private FileDataHandler fileHandler;
    private string fileName;
    private string pathDirName;
    private List<ISave> saves = new List<ISave>();
    private HashSet<ISave> loaded = new HashSet<ISave>();
    protected override void Awake()
    {
        MakeSingleton(false);
        saves = FindAllSave();
        
    }

    private void Start()
    {
        fileName = "GameData.json"; // tên file
        pathDirName = Application.persistentDataPath; // đường dẫn lưu trữ
        fileHandler = new FileDataHandler(pathDirName, fileName);

        LoadGame();
    }

    private void LoadGame()
    {
        gameData = fileHandler.LoadGame();

        if (gameData == null)
        {
            Debug.Log("New Game");
            NewGame();
        }
        else
        {
            if (saves.Count == 0) return;

            Debug.Log(gameData != null);

            Debug.Log("Load Data");
            foreach (ISave s in saves)
            {
                s.LoadGame(gameData);
            }
        }
    }

    private void NewGame()
    {
        gameData = new GameData();
    }


    private List<ISave> FindAllSave()
    {
        IEnumerable<ISave> isaves = Resources.FindObjectsOfTypeAll<MonoBehaviour>().OfType<ISave>().ToList();
        List<ISave> tmp = new List<ISave>(isaves);

        return tmp;
    }

    public void SaveGame()
    {
        if (saves.Count == 0) return;
        Debug.Log("Save Game");

        foreach (ISave s in saves)
        {
            s.SaveGame(ref gameData);
        }

        string jsonData = JsonUtility.ToJson(gameData, true);
        Debug.Log("GameData before saving: " + jsonData);
        fileHandler.SaveData(gameData);
    }

    protected override void OnApplicationQuit()
    {
        SaveGame();
    }



}
