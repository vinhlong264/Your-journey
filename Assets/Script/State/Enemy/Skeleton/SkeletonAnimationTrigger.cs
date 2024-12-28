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
        Collider2D[] attack = Physics2D.OverlapCircleAll(skeleton.AttackCheck.position, skeleton.attackRadius , whatIsMask);
        foreach(Collider2D hit in attack)
        {
            IDameHandlePhysical player = hit.GetComponent<IDameHandlePhysical>();
            if (player != null)
            {
                player.DoDamePhysical(skeleton.GetComponent<EnemyStats>());
            }
        }
    }

    public void OpenCounterAttack() => skeleton.OpenCounterAttack();

    public void CloseCounterAttack() => skeleton.CloseCounterAttack();
}
