using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();

        if (Input.GetKeyDown(KeyCode.Space)) // trạng thái jumpWall sẽ được thực thi và sau đó thoát luôn khỏi hàm này
        {
            _stateMachine.changeState(_player._jumpWallState);
            return;
        }

        if (InputX != 0 && InputX != _player.isFacingDir) // điều kiện xử lý của wallSlide để chuyển về Idle
        {
            _stateMachine.changeState(_player._idleState);
        }

        if (_player.groundCheck())
        {
            _stateMachine.changeState(_player._idleState);
        }

        if(InputY < 0)
        {
            rb.velocity = new Vector2(0 , rb.velocity.y);
        }
        else
        {
            rb.velocity = new Vector2(0, rb.velocity.y * 0.7f); 
        }
        
    }

    public override void Exit()
    {
        base.Exit();
        
    }
}
