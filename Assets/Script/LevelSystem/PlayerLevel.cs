using UnityEngine;

public class PlayerLevel : LevelAbstract, ISave
{
    private CharacterStats stats;
    [SerializeField] private LevelData levelData;

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
        if (levelData.level.gainExp(expReceive))
        {
            Debug.Log("Level up");
        }

        Observer.Instance.NotifyEvent(GameEvent.UpdateCurrentExp, null);
    }

    public override bool levelUpStats(StatType type)
    {
        if (levelData.level.pointAtributte > 0)
        {
            Debug.Log("Enough point Atributte");
            GetCharacterStats(type).addModifiers(1);
            levelData.level.pointAtributte--;
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
        if (levelData.level.pointSkill > 0)
        {
            levelData.level.pointSkill--;
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
            levelData.level = data.level;
        }
    }

    public void SaveGame(ref GameData data)
    {
        Debug.Log("Save data: " + this.gameObject.name);
        data.level = levelData.level;
    }

}
