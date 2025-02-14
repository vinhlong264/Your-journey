using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRunState : SkeletonGroundState
{
    public SkeletonRunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Skeleton skeleton) : base(enemyBase, stateMachine, animationBoolName, skeleton)
    {
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
        if (_skeleton.wallCheck() || !_skeleton.groundCheck())
        {
            //Kiểm tra nếu thỏa mãm 1 trong 2 điểu kiện chuyển qua state và reset biến stateTimer về giá trị của nó được đặt ở state idle
            _skeleton.Flip();
            stateMachine.changeState(_skeleton.idleState);
        }
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
        _skeleton.rb.velocity = new Vector2(_skeleton.isFacingDir * _skeleton.moveSpeed, _skeleton.rb.velocity.y);
    }
}
