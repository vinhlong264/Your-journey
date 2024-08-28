using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAnimationTrigger : MonoBehaviour
{
    private Skeleton skeleton;
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
        Collider2D[] attack = Physics2D.OverlapCircleAll(skeleton.AttackCheck.position, skeleton.attackRadius);
        foreach(Collider2D hit in attack)
        {
            if(hit.GetComponent<Player>() != null)
            {
                PlayerStatus playerStatus = hit.GetComponent<PlayerStatus>();
                skeleton.status.DoDame(playerStatus);
            }
        }
    }

    public void OpenCounterAttack() => skeleton.OpenCounterAttack();

    public void CloseCounterAttack() => skeleton.CloseCounterAttack();
}
