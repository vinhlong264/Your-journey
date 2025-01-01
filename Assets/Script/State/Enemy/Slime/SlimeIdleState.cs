using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeIdleState : SlimeGroundState
{
    public SlimeIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Slime slime) : base(enemyBase, stateMachine, animationBoolName, slime)
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

        if(stateTimer < 0f)
        {
            stateMachine.changeState(_slime.runState);
        }
    }

    public override void Exit()
    {
       base.Exit();
    }

    public override void AnimationTriggerCalled()
    {
        
    }
}
