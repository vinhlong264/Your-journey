using UnityEngine;

public class Dash_Skill : Skill
{
    [Header("Dash")]
    [SerializeField] private UI_SkillTreeSlot dashSkillUnlockBtn;
    public bool dashUnlock { get; private set; }

    [Header("Dash create clone")]
    [SerializeField] private UI_SkillTreeSlot dashCreateCloneBtn; 
    public bool dashCloneUnlock { get; private set; }

    [Header("Dash create clone arival")]
    [SerializeField] private UI_SkillTreeSlot dashCreateCloneArrivalBtn;
    public bool dashCloneArirvalUnlock {  get; private set; }

    protected override void Start()
    {
        base.Start();
        dashSkillUnlockBtn.eventUnlockSKill += onDashUnlock;
        dashCreateCloneBtn.eventUnlockSKill += onDashCloneUnlock;
        dashCreateCloneArrivalBtn.eventUnlockSKill += onDashCloneArirval;
    }
    public override void UseSkill()
    {
        base.UseSkill();
        //Debug.Log("đã sử dụng skill");
        coolDownTimer = coolDown;
    }
    #region Unlock Skill
    private void onDashUnlock() // Mở khóa skill Dash
    {
        if (dashSkillUnlockBtn.isUnlocked)
        {
            dashUnlock = true;
        }
    }

    private void onDashCloneUnlock() // Mở khóa skill ngay khi Dash sẽ tạo ra clone
    {
        if (dashCreateCloneBtn.isUnlocked)
        {
            dashCloneUnlock = true;
        }
    }

    private void onDashCloneArirval() // Mở khóa skill tạo ra clone khi Dash đến điểm kết thúc
    {
        if (dashCreateCloneArrivalBtn.isUnlocked)
        {
            dashCloneArirvalUnlock = true;
        }
    }

    public void dashCreateClone()
    {
        if (dashCloneUnlock)
        {
            skillManager.clone_skill.CreateClone(player.transform, Vector3.zero);
        }
    }

    public void dashCreateOnArrival()
    {
        if (dashCloneArirvalUnlock)
        {
            skillManager.clone_skill.CreateClone(player.transform, Vector3.zero);
        }
    }

    #endregion 

}
