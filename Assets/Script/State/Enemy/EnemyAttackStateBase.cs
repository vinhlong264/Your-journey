using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackStateBase : EnemyState
{
    public EnemyAttackStateBase(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName) : base(enemyBase, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        enemyBase.setZeroVelocity();
        if (triggerCalled)
        {
            stateMachine.changeState(enemyBase._battleAttackBase);
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemyBase.lastTime = Time.time;
    }


}
