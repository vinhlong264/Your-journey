using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BottleHotKey : UI_SkillHotKeyAbstract
{
    protected override void HandelInput()
    {
        if (Input.GetKeyDown(SkillHotKey))
        {
            setCoolDownOf(skillImageCoolDown);
        }

        checkOfCoolDownOf(skillImageCoolDown, Inventory.Instance.LastTimeUseBollte);
    }
}
