using UnityEngine;

public class PlayerLevel : LevelAbstract, ISave
{

    private void OnEnable()
    {
        SaveManager.Instance.addSave(this);

        Observer.Instance.subscribeListener(GameEvent.RewardExp, LevelUp);
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

    protected override void levelUpStats(CharacterStats stat)
    {
        
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
    }

}
