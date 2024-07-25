using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : SlimeGroundState
{
    public SlimeIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Enemy_Slime slime) : base(enemyBase, stateMachine, animationBoolName, slime)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = slime.idleTimer;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            stateMachine.changeState(slime.runState);
        }
    }
}
