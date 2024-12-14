public class PlayerDashState : PlayerState
{
    public PlayerDashState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player.skill.dash_skill.dashCreateClone(); // Tạo ra clone ở ngay khi Dash được kích hoạt
        stateTimer = _player.dashDuration;
    }

    public override void Execute()
    {
        base.Execute();
        _player.setVelocity(_player.dashSpeed * _player.dashDirection, 0); // tốc độ Dash
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
        _player.setVelocity(0, rb.velocity.y); // Kết thúc Dash thì gia tốc x sẽ về 0
        _player.skill.dash_skill.dashCreateOnArrival();
    }
}
