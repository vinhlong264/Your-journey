using UnityEngine;
public class Skill : MonoBehaviour
{
    protected float coolDownTimer;
    [SerializeField] protected float coolDown;
    protected Player player;

    protected virtual void Start()
    {
        player = PlayerManager.instance.player;
    }
    
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
