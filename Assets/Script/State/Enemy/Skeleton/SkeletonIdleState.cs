using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonIdleState : SkeletonGroundState
{
    public SkeletonIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Skeleton enemy) : base(enemyBase, stateMachine, animationBoolName, enemy)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f; // thời gian chuyển tiếp giữa Idle và run
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0f) // khi stateTimer < 0 sẽ chuyển
        {
            stateMachine.changeState(enemy.runState);
        }
    }
}
