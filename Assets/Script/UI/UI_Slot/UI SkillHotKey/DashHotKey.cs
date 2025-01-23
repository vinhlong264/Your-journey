using UnityEngine;

public class DashHotKey : UI_SkillHotKeyAbstract
{
    protected override void OnEnable()
    {
        base.OnEnable();
        if (skillImage != null)
        {
            if (skill.dash_skill.dashUnlock)
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
        if(Input.GetKeyDown(SkillHotKey) && skill.dash_skill.dashUnlock)
        {
            setCoolDownOf(skillImageCoolDown);
        }

        checkOfCoolDownOf(skillImageCoolDown , skill.dash_skill.CoolDown);
    }
}
