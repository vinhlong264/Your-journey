using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimSwordState : PlayerState
{
    public PlayerAnimSwordState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Execute()
    {
        base.Execute();
        if (Input.GetKeyUp(KeyCode.Mouse1)) 
        {
            _stateMachine.changeState(_player._idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
