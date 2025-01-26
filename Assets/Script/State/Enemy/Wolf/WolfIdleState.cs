using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfIdleState : WolfGroundState
{
    public WolfIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Wolf wolf) : base(enemyBase, stateMachine, animationBoolName, wolf)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            stateMachine.changeState(_wolf._runState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

}
