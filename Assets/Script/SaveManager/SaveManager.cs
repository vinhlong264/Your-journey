using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : Singleton<SaveManager>
{
    private GameData gameData;
    private FileDataHandler fileHandler;
    private string fileName;
    private string pathDirName;
    private List<ISave> saves = new List<ISave>();
    protected override void Awake()
    {
        MakeSingleton(false);
    }

    private void OnEnable()
    {

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
            foreach(ISave save in saves)
            {
                save.LoadGame(gameData);
            }
        }
    }

    public void addSave(ISave save)
    {
        saves.Add(save);
    }

    private void NewGame()
    {
        gameData = new GameData();
    }

    public void SaveGame()
    {
        foreach (ISave save in saves)
        {
            save.SaveGame(ref gameData);
        }

        fileHandler.SaveData(gameData);
    }

    protected override void OnApplicationQuit()
    {
        SaveGame();
    }



}
