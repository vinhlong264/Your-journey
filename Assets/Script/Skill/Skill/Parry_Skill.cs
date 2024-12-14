using UnityEngine;

public class Parry_Skill : Skill
{
    [Header("Parry")]
    [SerializeField] private UI_SkillTreeSlot parryUnlockSkillBtn;
    public bool parryUnlock { get; private set; }

    [Header("Parry with restore health")]
    [SerializeField] private UI_SkillTreeSlot parryRestoreSkillBtn;
    public bool parryRestoreUnlock { get; private set; }

    [Header("Parry with create Mirage")]
    [SerializeField] private UI_SkillTreeSlot parryWithMirageSkillBtn;
    public bool parryWithMirageUnlock { get; private set; }

    protected override void Start()
    {
        base.Start();
        parryUnlockSkillBtn.eventUnlockSKill += onUnlockParry;
        parryRestoreSkillBtn.eventUnlockSKill += onUnlockParryRestore;
        parryWithMirageSkillBtn.eventUnlockSKill += onUnlockParryWithMirage;
    }

    protected override void UseSkill()
    {
        base.UseSkill();

        //if (parryRestoreUnlock)
        //{
            
        //}
    }

    private void onUnlockParry()
    {
        if (parryUnlockSkillBtn.isUnlocked)
        {
            parryUnlock = true;
        }
    }

    private void onUnlockParryRestore()
    {
        if (parryRestoreSkillBtn.isUnlocked)
        {
            parryRestoreUnlock = true;
        }
    }

    private void onUnlockParryWithMirage()
    {
        if (parryWithMirageSkillBtn.isUnlocked)
        {
            parryWithMirageUnlock = true;
        }
    }

    public void parryCreatMirage(Transform _target)
    {
        if (parryWithMirageUnlock)
        {
            skillManager.clone_skill.CreatCloneWhenMirrage(_target);
        }
    }


}
