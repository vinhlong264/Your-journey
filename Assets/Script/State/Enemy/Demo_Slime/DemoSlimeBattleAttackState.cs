using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSlimeBattleAttackState : EnemyState
{
    private DemoSlime demoSlime;
    private float moveDir;
    private Player player;
    public DemoSlimeBattleAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, DemoSlime demoSlime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.demoSlime = demoSlime;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameManager.Instance.player;
        moveDir = 1;
    }

    public override void Update()
    {
        base.Update();
        if (demoSlime.isPlayerDetected())
        {
            if (demoSlime.isPlayerDetected().distance < demoSlime.RangeSafe)
            {
                if (CanJump())
                {
                    Debug.Log("Change state: JumpState");
                    stateMachine.changeState(demoSlime._jumpState);
                    return;
                }
            }

            if (demoSlime.isPlayerDetected().distance < demoSlime.AttackDis)
            {
                if (canAttack())
                {
                    Debug.Log("Change state: Attack State");
                    stateMachine.changeState(demoSlime._attackBulletState);
                }
            }
        }

        if(player.transform.position.x > demoSlime.transform.position.x)
        {
            moveDir = 1f;
        }
        else if(player.transform.position.x < demoSlime.transform.position.x)
        {
            moveDir = -1f;
        }

        demoSlime.setVelocity(moveDir, demoSlime.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }


    private bool CanJump()
    {
        Debug.Log($"Time: {Time.time} - CoolDown: {demoSlime.lastTimeJump + demoSlime.JumpTimeCoolDown}");
        if(Time.time > demoSlime.lastTimeJump + demoSlime.JumpTimeCoolDown)
        {
            demoSlime.lastTimeJump = Time.time;
            return true;
        }
        return false;
    }
}
