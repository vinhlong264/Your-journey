using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRunStateBase : EnemyGroundStateBase
{
    public EnemyRunStateBase(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName) : base(enemyBase, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        enemyBase.setVelocity(enemyBase.moveSpeed * enemyBase.isFacingDir, enemyBase.rb.velocity.y);
        if(!enemyBase.groundCheck() || enemyBase.wallCheck())
        {
            enemyBase.Flip();
            stateMachine.changeState(enemyBase._idleStateBase);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
