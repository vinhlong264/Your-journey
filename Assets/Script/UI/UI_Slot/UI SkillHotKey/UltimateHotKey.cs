using UnityEngine;
public class UltimateHotKey : UI_SkillHotKeyAbstract
{
    protected override void OnEnable()
    {
        base.OnEnable();

        Debug.Log("Call");
        if(skillImage != null)
        {
            if (skill.blackHole_skill.blackHoleUnlocked)
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
        if (Input.GetKeyDown(SkillHotKey) && skill.blackHole_skill.blackHoleUnlocked)
        {
            setCoolDownOf(skillImageCoolDown);
        }

        checkOfCoolDownOf(skillImageCoolDown, skill.blackHole_skill.CoolDown);
    }
}
