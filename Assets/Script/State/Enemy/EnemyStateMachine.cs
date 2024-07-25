using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStateMachine
{
    public EnemyState currentState {  get; private set; }
    public void initialize(EnemyState stateStart)
    {
        currentState = stateStart;
        currentState.Enter();
    }

    public void changeState(EnemyState stateNew)
    {
        currentState.Exit();
        currentState = stateNew;
        currentState.Enter();
    }
}
