
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private Player player;
    [SerializeField] private LayerMask whatIsMask;
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
        Collider2D[] col = Physics2D.OverlapCircleAll(player.AttackCheck.position, player.attackRadius , whatIsMask);
        foreach(Collider2D hit in col)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                EnemyStats enemyStatus = hit.GetComponent<EnemyStats>();

                if (enemyStatus == null) return;

                playerStats.DoDamePhysical(enemyStatus); // damage physical
                //playerStats.doDameMagical(enemyStatus); // dame magiacal

                ItemEquipmentSO equipment = Inventory.Instance.getEquipmentBy(EqipmentType.Sword);
                if (equipment != null)
                {
                    equipment.excuteItemEffect(enemyStatus.transform);
                }

            }
        }
    }

    private void ThrowSword() // skill ném kiếm
    {
        SkillManager.instance.sword_Skill.createSword();
    }
    
}
