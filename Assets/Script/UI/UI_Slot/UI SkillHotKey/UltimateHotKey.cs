using UnityEngine;
public class UltimateHotKey : UI_SkillHotKeyAbstract
{
    protected override void HandelInput()
    {
        if (Input.GetKeyDown(SkillHotKey) && skill.blackHole_skill.blackHoleUnlocked)
        {
            setCoolDownOf(skillImageCoolDown);
        }

        checkOfCoolDownOf(skillImageCoolDown, skill.blackHole_skill.CoolDown);
    }
}
