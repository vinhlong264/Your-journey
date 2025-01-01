using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    private Skeleton skeleton;
    [SerializeField] private LayerMask whatIsMask;
    private void Start()
    {
        skeleton = GetComponentInParent<Skeleton>();
    }
    public void AnimationTrigger()
    {
        skeleton.animationTriggerFinish();
    }

    public void AttackTrigger()
    {
        Collider2D[] attack = Physics2D.OverlapCircleAll(skeleton.AttackChecks.position, skeleton.AttackRadius , whatIsMask);
        foreach(Collider2D hit in attack)
        {
            IDameHandlePhysical targetReceive = hit.GetComponent<IDameHandlePhysical>();
            if (targetReceive != null)
            {
                targetReceive.DoDamePhysical(skeleton.GetComponent<EnemyStats>());
            }
        }
    }

    public void OpenCounterAttack() => skeleton.OpenCounterAttack();

    public void CloseCounterAttack() => skeleton.CloseCounterAttack();
}
