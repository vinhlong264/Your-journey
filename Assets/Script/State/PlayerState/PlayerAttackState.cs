using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttacked;
    private float attackWindow = 2;
    public PlayerAttackState(Player player, PlayerStateMachine stateMachine, string animationBoolName) : base(player, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if(comboCounter > 2 || Time.time >= lastTimeAttacked + attackWindow)
        //Nếu đã vượt qua số lần của comboCounter sẽ reset nó về 0 hoặc thời gian hiện tại vượt qua tổng attackWindow thì cx reset về 0
        {
            comboCounter = 0;
        }
        _player.animator.SetInteger("comboCounter" , comboCounter);
        _player.setVelocity(_player.attackMovement[comboCounter] * _player.isFacingDir, rb.velocity.y); // khi tấn công sẽ tiến lên 1 chút
        stateTimer = 0.1f;
    }

    public override void Execute()
    {
        base.Execute();
        
        if(stateTimer < 0)
        {
            rb.velocity = Vector2.zero;
        }

        if (triggerCalled)
        {
            _stateMachine.changeState(_player._idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++; 
        lastTimeAttacked = Time.time;
        _player.StartCoroutine("isBusyFor", 0.2f);
    }
}
