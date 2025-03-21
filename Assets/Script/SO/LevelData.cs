using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "UserData/LevelData")]
public class LevelData : ScriptableObject
{
    public LevelSystem level;
    public SerializableDictionary<StatType, int> stats;
}
