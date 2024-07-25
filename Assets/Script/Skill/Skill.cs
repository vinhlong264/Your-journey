using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Accessibility;

public class Skill : MonoBehaviour
{
    protected float coolDownTimer;
    [SerializeField] protected float coolDown;
    
    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }

    public virtual bool CanUseSkill()
    {
        if(coolDownTimer < 0f)
        {
            //Sử dụng skill
            UseSkill();
            coolDownTimer = coolDown;
            return true;
        }
        return false;
    }

    public virtual void UseSkill()
    {

    }
}
