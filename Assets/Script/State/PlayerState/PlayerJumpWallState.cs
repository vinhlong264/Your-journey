using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpWallState : PlayerState
{
    public PlayerJumpWallState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.4f;
        _player.setVelocity(5f *  -_player.isFacingDir, _player.jumpForce);
    }

    public override void Execute()
    {
        base.Execute();

        if (!_player.wallCheck())
        {
            _stateMachine.changeState(_player._idleState);
        }

        if (stateTimer < 0)
        {
            _stateMachine.changeState(_player._airState);
        }

        if (_player.groundCheck())
        {
            _stateMachine.changeState(_player._idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
