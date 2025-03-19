using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

    private void Start()
    {
        fileName = "GameData.json"; // tên file
        pathDirName = Application.persistentDataPath; // đường dẫn lưu trữ
        fileHandler = new FileDataHandler(pathDirName, fileName);


        LoadGame();
    }

    public void addSubISave(ISave s)
    {
        saves.Add(s);
        Debug.Log(saves.Count);
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

    public void SaveGame()
    {
        Debug.Log("Save Game");
        if (saves.Count == 0) return;

        foreach (ISave s in saves)
        {
            s.SaveGame(ref gameData);
        }

        fileHandler.SaveData(gameData);
    }

    protected override void OnApplicationQuit()
    {
        SaveGame();
    }



}
