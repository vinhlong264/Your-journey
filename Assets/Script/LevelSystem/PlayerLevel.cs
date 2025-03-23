using UnityEngine;

public class PlayerLevel : LevelAbstract, ISave
{
    private CharacterStats stats;
    private LevelSystem level;

    public LevelSystem Level { get => level; set => level = value; }

    private void OnEnable()
    {
        Observer.Instance.subscribeListener(GameEvent.RewardExp, LevelUp);
    }

    void Start()
    {
        stats = GetComponent<CharacterStats>();
    }


    protected override void LevelUp(object value)
    {
        int expReceive = (int)value;
        if (level.gainExp(expReceive))
        {
            Debug.Log("Level up");
        }

        Observer.Instance.NotifyEvent(GameEvent.UpdateCurrentExp, null);
    }

    public override bool levelUpStats(StatType type)
    {
        if (level.pointAtributte > 0)
        {
            Debug.Log("Enough point Atributte");
            GetCharacterStats(type).addModifiers(1);
            level.pointAtributte--;
            return true;
        }

        Debug.Log("No Enough point Atributte");
        return false;
    }

    private Stats GetCharacterStats(StatType t)
    {
        if(stats == null) return null;

        if(t == StatType.Strength)
        {
            return stats.strength;
        }
        else if(t == StatType.Ability)
        {
            return stats.ability;
        }
        else if(t == StatType.inteligent)
        {
            return stats.inteligent;
        }
        else if(t == StatType.vitality)
        {
            return stats.vitality;
        }

        return null;
    }

    public override bool unlockSkill(Skilltype skill)
    {
        if (level.pointSkill > 0)
        {
            level.pointSkill--;
            return true;
        }

        return false;
    }

    public void LoadGame(GameData data)
    {     
        if(data == null)
        {
            Debug.Log("No Data");
            return;
        }
        else
        { 
            level = data.level;
        }
    }

    public void SaveGame(ref GameData data)
    {
        data.level = level;
    }

}
