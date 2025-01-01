using UnityEngine;

public abstract class AnimationEventBase : MonoBehaviour
{
    [SerializeField] protected Enemy enemyBase;
    [SerializeField] protected LayerMask whatIsMask;
    protected virtual void Start()
    {
        enemyBase = GetComponentInParent<Enemy>();
    }

    protected void animationTriggerFinish() => enemyBase.animationTriggerFinish();

    protected virtual void AttackEventTrigger() 
    {
        if(enemyBase == null) return;

        Collider2D[] attack = Physics2D.OverlapCircleAll(enemyBase.AttackChecks.position, enemyBase.AttackRadius, whatIsMask);
        foreach (Collider2D hit in attack)
        {
            IDameHandlePhysical targetReceive = hit.GetComponent<IDameHandlePhysical>();
            if (targetReceive != null)
            {
                targetReceive.DoDamePhysical(enemyBase.GetComponent<EnemyStats>());
            }
        }
    }


}
