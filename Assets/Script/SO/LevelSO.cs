using UnityEngine;

[CreateAssetMenu(fileName = "Level Data", menuName = "User")]
public class LevelSO : ScriptableObject
{
    public LevelSystem levelSystem;
    public SerializableDictionary<StatType, int> stats;
}
