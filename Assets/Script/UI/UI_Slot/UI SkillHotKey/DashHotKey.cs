using UnityEngine;

public class DashHotKey : UI_SkillHotKeyAbstract
{
    protected override void Start()
    {
        base.Start();
        
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
