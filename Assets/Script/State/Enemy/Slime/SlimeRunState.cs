using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeRunState : SlimeGroundState
{
    public SlimeRunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Slime slime) : base(enemyBase, stateMachine, animationBoolName, slime)
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
        if (!_slime.groundCheck() || _slime.wallCheck())
        {
            _slime.Flip();
            stateMachine.changeState(_slime.idleState);
        }
    }

    public override void FixUpdate()
    {
        _slime.setVelocity(_slime.moveSpeed * _slime.isFacingDir, _slime.rb.velocity.y);
    }
}
