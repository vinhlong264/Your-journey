using UnityEngine;

public class BottleHotKey : UI_SkillHotKeyAbstract
{
    protected override void HandelInput()
    {
        if (Input.GetKeyDown(SkillHotKey))
        {
            setCoolDownOf(skillImageCoolDown);
        }

        checkOfCoolDownOf(skillImageCoolDown, inventory.LastTimeUseBollte);
    }
}
