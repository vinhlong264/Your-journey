using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttackState : EnemyState
{
    private Skeleton enemy;
    public SkeletonAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Skeleton enemy) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        enemy.lastTime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        enemy.setZeroVelocity(); // khi vào state attack Enemy sẽ ngừng di chuyển

        if(triggerCalled)
        {
            stateMachine.changeState(enemy.battleState);
        }
    }
}

