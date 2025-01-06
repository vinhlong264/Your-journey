using UnityEngine;

public class ThrowSwordHotKey : UI_SkillHotKeyAbstract
{
    protected override void HandelInput()
    {
        if (Input.GetKeyDown(SkillHotKey) && skill.sword_Skill.swordSkillUnlocked)
        {
            setCoolDownOf(skillImageCoolDown);
        }

        if (skill.sword_Skill.isUsing) return;

        checkOfCoolDownOf(skillImageCoolDown, skill.sword_Skill.CoolDown);
    }
}
