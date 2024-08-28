using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime_AnimationTrigger : MonoBehaviour
{
    private Enemy_Slime slime;
    void Start()
    {
        slime = GetComponentInParent<Enemy_Slime>();
    }

    public void AnimationFinsih() => slime.animationTriggerFinish();

    // Update is called once per frame
    public void attackTrigger()
    {
        Collider2D[] hit = Physics2D.OverlapCircleAll(slime.AttackCheck.position, slime.attackRadius);
        foreach(Collider2D col in hit)
        {
            if(col.GetComponent<Player>() != null)
            {
                PlayerStatus playerStatus = col.GetComponent<PlayerStatus>();
                playerStatus.DoDame(playerStatus); 
            }
        }
    }
}
