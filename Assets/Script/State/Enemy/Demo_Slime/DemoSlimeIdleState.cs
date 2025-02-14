using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSlimeIdleState : DemoSlimeGroundState
{
    public DemoSlimeIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, DemoSlime demoSlime) : base(enemyBase, stateMachine, animationBoolName, demoSlime)
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
            stateMachine.changeState(demoSlime._runState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
