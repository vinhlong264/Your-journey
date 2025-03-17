using UnityEngine;

public class PlayerLevel : LevelAbstract, ISave
{
    private CharacterStats stats;

    private void OnEnable()
    {
        Observer.Instance.subscribeListener(GameEvent.RewardExp, LevelUp);
        SaveManager.Instance.addSubISave(this);
    }

    void Start()
    {
        stats = GetComponent<CharacterStats>();
    }


    protected override void LevelUp(object value)
    {
        int expReceive = (int)value;
        if (level.levelSystem.gainExp(expReceive))
        {
            Debug.Log("Level up");
        }

        Observer.Instance.NotifyEvent(GameEvent.UpdateCurrentExp, null);
    }

    public override bool levelUpStats(StatType type)
    {
        if(level.levelSystem.pointAtributte > 0)
        {
            Debug.Log("Enough point Atributte");
            GetCharacterStats(type).addModifiers(1);
            level.levelSystem.pointAtributte--;
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
        if(level.levelSystem.pointSkill > 0)
        {
            return true;
        }

        return false;
    }

    public void LoadGame(GameData data)
    {
        if(data == null)
        {
            level.levelSystem = new LevelSystem();
        }
        else
        {
            Debug.Log("Load data: " + this.gameObject.name);
            level.levelSystem = data.level;
        }
    }

    public void SaveGame(ref GameData data)
    {
        Debug.Log("Save data: " + this.gameObject.name);
        data.level = level.levelSystem;
    }

}
