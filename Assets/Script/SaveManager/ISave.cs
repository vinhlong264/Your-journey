using System.Collections.Generic;
using UnityEngine;

public interface ISave
{
    void LoadGame(GameData data);
    void SaveGame(ref GameData data);
}

[System.Serializable]
public class GameData
{
    public LevelSystem level;
    public SerializableDictionary<string, int> inventorys;
    public List<string> equipmentID;
    public SerializableDictionary<string, bool> skills;
    public SerializableDictionary<StatType, int> stats;

    public GameData()
    {
        level = new LevelSystem();
        inventorys = new SerializableDictionary<string, int>();
        equipmentID = new List<string>();
        skills = new SerializableDictionary<string, bool>();
        stats = new SerializableDictionary<StatType, int>();
    }
}

[System.Serializable]
public class SerializableDictionary<Tkey, Tvalue> : Dictionary<Tkey, Tvalue>, ISerializationCallbackReceiver
{
    [SerializeField] private List<Tkey> keys = new List<Tkey>();
    [SerializeField] private List<Tvalue> values = new List<Tvalue>();

    public void OnAfterDeserialize() // sau khi chuyển đổi dữ liệu
    {
        this.Clear();

        if (keys.Count != values.Count)
        {
            Debug.Log("Keys count is not equal to values count");
            return;
        }

        for (int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }

    }

    public void OnBeforeSerialize() // trước khi chuyển đổi dữ liệu
    {
        keys.Clear();
        values.Clear();

        foreach (KeyValuePair<Tkey, Tvalue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }
}
