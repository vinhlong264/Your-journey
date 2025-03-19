using UnityEngine;

[CreateAssetMenu(fileName = "LevelData", menuName = "UserData/LevelData")]
public class LevelData : ScriptableObject
{
    public LevelSystem level = new LevelSystem();
}
