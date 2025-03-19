using UnityEngine;

[CreateAssetMenu(fileName = "StatsData", menuName = "UserData/StatsData")]
public class StatsData : ScriptableObject
{
    public SerializableDictionary<StatType, int> stats = new SerializableDictionary<StatType, int>();
}
