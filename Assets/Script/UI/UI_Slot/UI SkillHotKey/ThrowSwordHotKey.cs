using UnityEngine;

public class ThrowSwordHotKey : UI_SkillHotKeyAbstract
{
    protected override void OnEnable()
    {
        base.OnEnable();
        if (skillImage != null)
        {
            if (skill.sword_Skill.swordSkillUnlocked)
            {
                skillImage.enabled = true;
            }
            else
            {
                skillImage.enabled = false;
            }
        }
    }

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
