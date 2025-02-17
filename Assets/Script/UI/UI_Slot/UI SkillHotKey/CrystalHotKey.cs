using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CrystalHotKey : UI_SkillHotKeyAbstract
{
    private bool isCompelete;
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

    protected override void Start()
    {
        skill.crystal_skill.CompeleteSkill += CompeleteSkillHandler;
    }

    protected override void HandelInput()
    {
        if (Input.GetKeyDown(SkillHotKey) && skill.crystal_skill.crystalSkillUnlocked)
        {
            setCoolDownOf(skillImageCoolDown);
        }

        if (!isCompelete) return;

        checkOfCoolDownOf(skillImageCoolDown, skill.crystal_skill.CoolDown);
    }


    private void CompeleteSkillHandler()
    {
        Debug.Log("Call back");
        isCompelete = true;
    }

    protected override void checkOfCoolDownOf(Image _image, float _coolDown)
    {
        base.checkOfCoolDownOf(_image, _coolDown);
        if (_image.fillAmount <= 0)
        {
            isCompelete = false;
        }
    }
}
