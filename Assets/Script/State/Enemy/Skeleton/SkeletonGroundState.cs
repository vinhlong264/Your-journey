using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    protected Skeleton enemy;
    public SkeletonGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName , Skeleton enemy) : base(enemyBase, stateMachine, animationBoolName)
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
    }

    public override void Update()
    {
        base.Update();
        if (enemy.isPlayerDetected())
        {
            stateMachine.changeState(enemy.battleState);
        }
    }
}
