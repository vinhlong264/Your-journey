using UnityEngine;

public class ThrowSwordHotKey : UI_SkillHotKeyAbstract
{
    protected override void HandelInput()
    {
        if (Input.GetKeyDown(SkillHotKey) && skill.sword_Skill.swordSkillUnlocked)
        {
            setCoolDownOf(skillImageCoolDown);
        }

        checkOfCoolDownOf(skillImageCoolDown, skill.sword_Skill.CoolDown);
    }
}
