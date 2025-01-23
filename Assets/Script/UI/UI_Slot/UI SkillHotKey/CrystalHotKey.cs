using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHotKey : UI_SkillHotKeyAbstract
{
    protected override void OnEnable()
    {
        base.OnEnable();
        if (skillImage != null)
        {
            if (skill.crystal_skill.crystalSkillUnlocked)
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
        if (Input.GetKeyDown(SkillHotKey) && skill.crystal_skill.crystalSkillUnlocked)
        {
            setCoolDownOf(skillImageCoolDown);
        }

        checkOfCoolDownOf(skillImageCoolDown, skill.crystal_skill.CoolDown);
    }
}
