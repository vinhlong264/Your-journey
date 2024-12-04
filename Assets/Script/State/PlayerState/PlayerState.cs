using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine _stateMachine; // khởi tạo biến theo kiểu class của PlayerStateMachine
    protected Player _player; //khởi tạo biến với kiểu dữ liệu class Player
    protected Rigidbody2D rb;

    protected float InputX;
    protected float InputY;
    protected bool triggerCalled;
    protected float stateTimer; //Biến quản lý thời gian chuyển state Dash về Idle
    protected string _animationBoolName; // biến tên của animation được chuyển tiếp

    public PlayerState(Player player, PlayerStateMachine stateMachine, string animationBoolName) 
    {
        _stateMachine = stateMachine;
        _player = player;
        _animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        //Debug.Log("I enter " + _animationBoolName);
        stateTimer = 0;
        _player.animator.SetBool(_animationBoolName, true);
        rb = _player.rb;
        triggerCalled = false;
    }

    public virtual void Execute()
    {
        stateTimer -= Time.deltaTime;
        //Debug.Log("I in " + _animationBoolName);
        InputX = Input.GetAxisRaw("Horizontal");
        InputY = Input.GetAxisRaw("Vertical");
        _player.animator.SetFloat("y.velocity", rb.velocity.y);
        //Debug.Log(triggerCalled);
    }

    public virtual void Exit()
    {
        //Debug.Log("I exit " + _animationBoolName);
        _player.animator.SetBool(_animationBoolName, false);
    }

    public virtual void animationTriggerEvent()
    {
        triggerCalled = true;
       
    }

}
