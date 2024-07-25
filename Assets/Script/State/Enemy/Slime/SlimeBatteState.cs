using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SlimeBatteState : EnemyState
{
    private Enemy_Slime slime;
    private Transform Player;
    public float moveAttackDir;

    public SlimeBatteState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName , Enemy_Slime slime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.slime = slime;
    }

    public override void Enter()
    {
        base.Enter();
        Player = GameObject.Find("Player").transform;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (slime.isPlayerDetected())
        {
            stateTimer = slime.battleTime;
            if (slime.isPlayerDetected().distance < slime.attackDis)
            {
                if (canAttack())
                {
                    stateMachine.changeState(slime.attackState);
                }
                    
            }
        }
        else if(stateTimer < 0 || Vector2.Distance(Player.transform.position , slime.transform.position) < 7)
        {
            stateMachine.changeState(slime.idleState);
        }
        


        if(Player.transform.position.x > slime.transform.position.x)
        {
            moveAttackDir = 1f;
        }
        else if((Player.transform.position.x < slime.transform.position.x))
        {
            moveAttackDir = -1f;
        }
        slime.setVelocity(moveAttackDir, slime.rb.velocity.y);
    }


    private bool canAttack()
    {
        if(Time.time >= slime.Lastime + slime.attackCooldown)
        {
            slime.Lastime = Time.time;
            return true;
        }
        return false;
    }
}
