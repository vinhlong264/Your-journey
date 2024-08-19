using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonRunState : SkeletonGroundState
{
    public SkeletonRunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Skeleton enemy) : base(enemyBase, stateMachine, animationBoolName, enemy)
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
        enemy.rb.velocity = new Vector2(enemy.isFacingDir * enemy.moveSpeed , enemy.rb.velocity.y);

        if(enemy.wallCheck() || !enemy.groundCheck())
        {
            //Kiểm tra nếu thỏa mãm 1 trong 2 điểu kiện chuyển qua state và reset biến stateTimer về giá trị của nó được đặt ở state idle
            enemy.Flip();
            stateMachine.changeState(enemy.idleState);
        }
    }
}
