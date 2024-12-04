using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "SkillSO", menuName = "Skill")]
public class skillBaseSO : ScriptableObject
{
    public string skillName;
    public Sprite skillImage;
    public int skillLevel;
    public int requirementUnlocked;
}
