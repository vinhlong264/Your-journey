using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParryHotKey : UI_SkillHotKeyAbstract
{
    protected override void HandelInput()
    {
        if (Input.GetKeyDown(SkillHotKey) && skill.parry_Skill.parryUnlock)
        {
            setCoolDownOf(skillImageCoolDown);
        }

        checkOfCoolDownOf(skillImageCoolDown, skill.parry_Skill.CoolDown);
    }
}
