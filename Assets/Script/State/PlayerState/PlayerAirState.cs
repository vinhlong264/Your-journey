using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
        if (_player.wallCheck() && !_player.groundCheck())
        {
            _stateMachine.changeState(_player._wallSlideState);
        }

        if(_player.groundCheck()) // nếu groundCheck == true sẽ chuyển về trạng thái idle
        {
            _stateMachine.changeState(_player._idleState);
        }

        if(InputX != 0)
        {
            rb.velocity = new Vector2(_player.moveSpeed * 0.8f * InputX , rb.velocity.y);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
