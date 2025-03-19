using UnityEngine;

public abstract class LevelAbstract : MonoBehaviour
{
    protected abstract void LevelUp(object value);
    public abstract bool levelUpStats(StatType type);
    public abstract bool unlockSkill(Skilltype skill);
}
