using System.IO;
using UnityEngine;

public class FileDataHandler
{
    private string fileName;
    private string pathDirName;

    public FileDataHandler(string _pathDirName , string fileName)
    {
        this.pathDirName = _pathDirName;
        this.fileName = fileName;
    }

    public void SaveData(GameData gameData)
    {
        string fullPath = Path.Combine(pathDirName, fileName);
        try
        {
            Directory.CreateDirectory(Path.GetDirectoryName(fullPath));
            string dataStore = JsonUtility.ToJson(gameData , true);

            using (FileStream fs = new FileStream(fullPath, FileMode.Create))
            {
                using (StreamWriter sw = new StreamWriter(fs))
                {
                    sw.Write(dataStore);
                }
            }

        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error save data with: {fullPath} " + e.ToString());
        }

    }

    public GameData LoadGame()
    {
        string fullPath = Path.Combine(pathDirName, fileName);

        GameData loadData = null;

        if (!FileIsExist()) return loadData;

        try
        {
            string data = "";
            using (FileStream fs = new FileStream(fullPath, FileMode.Open))
            {
                using (StreamReader sr = new StreamReader(fs))
                {
                    data = sr.ReadToEnd();
                }
            }

            loadData = JsonUtility.FromJson<GameData>(data);

        }
        catch (System.Exception e)
        {
            Debug.LogError("No found fileData: " + fullPath + " ," + e.ToString());
        }

        return loadData;
    }


    public bool FileIsExist()
    {
        string fullPath = Path.Combine(pathDirName, fileName);

        if (File.Exists(fullPath))
        {
            return true;
        }

        return false;
    }
}
