using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLevel : LevelAbstract
{
    [SerializeField] private int pointExp;
    [SerializeField] private int pointSkill;

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
        level = 1;
        expCurrent = 0;
        pointExp = 0;
        pointSkill = 0;
    }

    protected override void LevelUp(object value)
    {
        expCurrent += (int)value;

        if(expCurrent >= GetExpNextToLevel())
        {
            expCurrent -= GetExpNextToLevel();
            level++;
            pointExp += 5;
            pointSkill += 1;
        }

        Observer.Instance.NotifyEvent(GameEvent.UpdateUI , expCurrent);
    }

    protected override void levelUpStats(CharacterStats stat)
    {
        // cập nhập các stats khi lên cấp
    }
}
