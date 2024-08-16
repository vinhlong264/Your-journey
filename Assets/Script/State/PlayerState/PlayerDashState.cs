using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        SkillManager.instance.clone_skill.CreateClone(_player.transform);
        /* Hàm được gọi từ SkillManager thông qua singleton
       Hàm CreateClone có tham số truyền vào là transform nhằm lấy được vị trí của Player hiện tại để khi bắt đầu kích hoạt
       Dash sẽ tạo ra Clone ở thời điểm ấy*/
        stateTimer = _player.dashDuration; 
    }

    public override void Execute()
    {
        base.Execute();
        _player.setVelocity(_player.dashSpeed * _player.dashDirection, 0); // kiểm soát tốc độ di chuyển của dash
        //Debug.Log("I doing Dash");
        if (stateTimer < 0) // nếu stateTimer < 0 sẽ chuyển qua trạng thái idle
        {
            _stateMachine.changeState(_player._idleState);
        }

        if (!_player.groundCheck() && _player.wallCheck())
        {
            _stateMachine.changeState(_player._wallSlideState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _player.setVelocity(0, rb.velocity.y); // set về 0 nhằm tạo ra sự cân bằng khi di chuyển, khi hết dash sẽ dừng lại 1 nhịp
    }
}
