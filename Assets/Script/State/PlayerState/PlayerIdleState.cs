using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : PlayerGroundedState
{
    public PlayerIdleState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        rb.velocity = new Vector2(0, 0);
        //khi đặt velocity như này sẽ tạo ra khoảng trống cho việc khi thoát khỏi wallJump sẽ k bị đổi hướng nhìn
    }

    public override void Execute()
    {
        base.Execute();
        if (InputX == _player.isFacingDir && _player.wallCheck()) return; // Input bằng với hướng nhìn của player và wallCheck == true
                                                                          // sẽ thoát luôn khỏi hàm này

        if(InputX != 0 && !_player.isAttacked)
        {
            _player._stateMachine.changeState(_player._runState);
        }
            
    }

    public override void Exit()
    {
        base.Exit();
    }
}
