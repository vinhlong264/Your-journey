public interface ISave
{
    void LoadGame(GameData data);
    void SaveGame(ref GameData data);
}

[System.Serializable]
public class GameData
{
    public LevelSystem level;

    public GameData()
    {
        level = new LevelSystem();
    }
}
