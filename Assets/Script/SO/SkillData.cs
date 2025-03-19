using UnityEngine;

[CreateAssetMenu(fileName = "SkillData" , menuName = "UserData/SkillData")]
public class SkillData : ScriptableObject
{
    public SerializableDictionary<string, bool> skills = new SerializableDictionary<string, bool>();
}
