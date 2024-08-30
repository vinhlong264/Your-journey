using UnityEngine;

public class PlayerDestroyContinentState : PlayerState
{
    public PlayerDestroyContinentState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 0.5f;

    }
    public override void Execute()
    {
        base.Execute();
        _player.setVelocity(_player.dashSpeed * _player.isFacingDir, _player.rb.velocity.y);


        if (triggerCalled || stateTimer < 0)
        {
            _stateMachine.changeState(_player._idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
