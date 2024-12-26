using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHotKey : UI_SkillHotKeyAbstract
{
    protected override void HandelInput()
    {
        if (Input.GetKeyDown(SkillHotKey) && skill.crystal_skill.crystalSkillUnlocked)
        {
            setCoolDownOf(skillImageCoolDown);
        }

        checkOfCoolDownOf(skillImageCoolDown, skill.crystal_skill.CoolDown);
    }
}
