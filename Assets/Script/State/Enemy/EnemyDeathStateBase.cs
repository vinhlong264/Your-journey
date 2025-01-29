using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDeathStateBase : EnemyState
{
    public EnemyDeathStateBase(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName) : base(enemyBase, stateMachine, animationBoolName)
    {
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Enter()
    {
        base.Enter();
        enemyBase.rb.bodyType = RigidbodyType2D.Static;
        enemyBase.setZeroVelocity();
        enemyBase.cd.enabled = false;
        enemyBase.DeactiveMe();
    }
}
