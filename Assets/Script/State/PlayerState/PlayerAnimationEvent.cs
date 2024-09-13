
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private Player player;
    void Start()
    {
        player = GetComponentInParent<Player>();
    }

    public void AnimationTrigger() // hàm dùng để kết thúc animation của 1 state thông qua triggercall
    {
        if(player != null)
        {
            player.AnimationEventTrigger();
        }
    }


    public void attackTrigger() // Hàm tấn công
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(player.AttackCheck.position, player.attackRadius);
        foreach(Collider2D hit in col)
        {
            if(hit.GetComponent<Enemy>() != null)
            {
                EnemyStatus enemyStatus = hit.GetComponent<EnemyStatus>();
                player.status.DoDame(enemyStatus);

                //Inventory.Instance.getEquipmentBy(EqipmentType.Sword).excuteItemEffect();
            }
        }
    }

    private void ThrowSword() // skill ném kiếm
    {
        SkillManager.instance.sword_Skill.createSword();
    }
    
}
