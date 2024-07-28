
using UnityEngine;

public class AnimationEvent : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void AnimationTrigger()
    {
        if(player != null)
        {
            player.AnimationEventTrigger();
        }
    }


    public void attackTrigger()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(player.AttackCheck.position, player.attackRadius);
        foreach(Collider2D hit in col)
        {
            if(hit.GetComponent<IDamage>() != null)
            {
                hit.GetComponent<IDamage>().takeDame(5);
            }
        }
    }

    private void ThrowSword()
    {
        SkillManager.instance.sword_Skill.createSword();
    }
    
}
