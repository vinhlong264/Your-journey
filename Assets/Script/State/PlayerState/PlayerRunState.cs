using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunState : PlayerGroundedState
{
    public PlayerRunState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
        _player.setVelocity(InputX * _player.moveSpeed, rb.velocity.y);
        if(InputX == 0 || _player.wallCheck())
        {
            _player._stateMachine.changeState(_player._idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
