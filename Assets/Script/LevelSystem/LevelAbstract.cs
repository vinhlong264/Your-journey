using UnityEngine;

public abstract class LevelAbstract : MonoBehaviour
{
    [SerializeField] protected LevelSO level;
    protected abstract void LevelUp(object value);
    protected abstract void levelUpStats(CharacterStats stat);
}
