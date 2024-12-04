using UnityEngine;

public class PlayerDeathState : PlayerState
{
    public PlayerDeathState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        Debug.Log("Enter: "+_animationBoolName);
    }

    public override void Execute()
    {
        base.Execute();
        _player.setZeroVelocity();
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit: " + _animationBoolName);
    }
}
