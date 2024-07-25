using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpState : PlayerState
{
    public PlayerJumpState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(rb.velocity.x, _player.jumpForce);
    }

    public override void Execute()
    {
        base.Execute();
        if (rb.velocity.y < -0.1f)
        {
            _stateMachine.changeState(_player._airState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
