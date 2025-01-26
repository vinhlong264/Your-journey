using UnityEngine;

public class PlayerLevel : LevelAbstract
{
    [SerializeField] private int pointExp;
    [SerializeField] private int pointSkill;
    [SerializeField] private PlayerStats playerStats;

    public int PointExp { get => pointExp; }
    public int PointSkill { get => pointSkill; }
    private void OnEnable()
    {
        Observer.Instance.subscribeListener(GameEvent.RewardExp, LevelUp);
    }

    private void OnDisable()
    {
        Observer.Instance.unsubscribeListener(GameEvent.RewardExp, LevelUp);
    }

    private void Start()
    {
        playerStats = GetComponent<PlayerStats>();

        level = 1;
        expCurrent = 0;
        pointExp = 0;
        pointSkill = 0;
    }

    protected override void LevelUp(object value)
    {
        expCurrent += (int)value;

        if (expCurrent >= GetExpNextToLevel())
        {
            expCurrent -= GetExpNextToLevel();
            level++;
            pointExp += 5;
            pointSkill += 1;
        }
        Observer.Instance.NotifyEvent(GameEvent.UpdateUI, expCurrent);
    }

    protected override void levelUpStats(CharacterStats stat)
    {
        // cập nhập các stats khi lên cấp
    }

    public bool canLevelUpStats(StatType type)
    {
        if (pointExp > 0)
        {
            levelUpStats(type);
            return true;
        }
        return false;
    }

    private void levelUpStats(StatType type)
    {
        // Cập nhập stats bằng điểm PointExp
        Debug.Log("Level up stat: " + type.ToString());
        playerStats.increaseStats(type);
        pointExp--;
    }
}
