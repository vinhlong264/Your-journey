using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonGroundState : EnemyState
{
    protected Skeleton _skeleton;
    public SkeletonGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName , Skeleton skeleton) : base(enemyBase, stateMachine, animationBoolName)
    {
        this._skeleton = skeleton;
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
        if (_skeleton.isPlayerDetected())
        {
            stateMachine.changeState(_skeleton.battleState);
        }
    }
}
