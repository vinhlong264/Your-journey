using System.Collections;
using UnityEngine;

public class PlayerAnimationEvent : MonoBehaviour
{
    private Player player;
    [SerializeField] private LayerMask whatIsMask;
    private Inventory inventory;
    void Start()
    {
        player = GetComponentInParent<Player>();
        inventory = GameManager.Instance.Inventory;
    }

    public void AnimationTrigger() // hàm dùng để kết thúc animation của 1 state thông qua triggercall
    {
        if (player != null)
        {
            player.AnimationEventTrigger();
        }
    }


    public void attackTrigger() // Hàm tấn công
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(player.AttackCheck.position, player.attackRadius, whatIsMask);
        foreach (Collider2D hit in col)
        {
            if (hit.GetComponent<EnemyStats>() != null)
            {
                IDameHandlePhysical EnemyReceivePhysic = hit.GetComponent<IDameHandlePhysical>();
                if (EnemyReceivePhysic != null)
                {
                    EnemyReceivePhysic.DameHandlerPhysical(player.GetComponent<PlayerStats>());
                    ItemEquipmentSO equipment = inventory.getEquipmentBy(EqipmentType.Sword);
                    if (equipment != null)
                    {
                        equipment.excuteItemEffect(hit.transform);
                    }
                }

            }
        }
    }

    IEnumerator DeactiveCrountine(GameObject obj)
    {
        yield return new WaitForSeconds(0.3f);
        obj.SetActive(false);
    }

    private void ThrowSword() // skill ném kiếm
    {
        player.skill.sword_Skill.createSword();
    }

}
