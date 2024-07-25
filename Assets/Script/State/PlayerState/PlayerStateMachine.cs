using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState currentState {  get; private set; }

   public  void initialize(PlayerState _state) // hàm bắt đầu của 1 state hiện tại
    {
        currentState = _state; // gán currentState = state hiện tại
        currentState.Enter(); // gọi hàm Enter chạy state ấy
    }


    public void changeState(PlayerState _newState) // hàm chuyển state
    {
        currentState.Exit(); // thoát state hiện tại
        currentState = _newState; // gán currentState = _newState(state mới)
        currentState.Enter(); // gọi hàm Enter để chạy state ấy
    }
}
