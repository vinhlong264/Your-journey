using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHotKey : UI_SkillHotKeyAbstract
{
    protected override void OnEnable()
    {
        base.OnEnable();
        if (skillImage != null)
        {
            if (skill.parry_Skill.parryUnlock)
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
        if (Input.GetKeyDown(SkillHotKey) && skill.parry_Skill.parryUnlock)
        {
            setCoolDownOf(skillImageCoolDown);
        }

        checkOfCoolDownOf(skillImageCoolDown, skill.parry_Skill.CoolDown);
    }
}
