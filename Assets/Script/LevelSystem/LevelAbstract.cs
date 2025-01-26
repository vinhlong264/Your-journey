using UnityEngine;

public abstract class LevelAbstract : MonoBehaviour
{
    [SerializeField] protected int level;
    protected int expCurrent;

    public int Level { get => level; }
    public int ExpCurrent { get => expCurrent; }



    protected abstract void LevelUp(object value);
    protected abstract void levelUpStats(CharacterStats stat);

    protected int GetExpNextToLevel()
    {
        return (int)(Mathf.Pow(level, 2) * 10f);
    }
}
