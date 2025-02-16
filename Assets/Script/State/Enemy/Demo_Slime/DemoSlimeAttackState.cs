using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSlimeAttackState : EnemyState
{
    private DemoSlime demoSlime;

    public DemoSlimeAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, DemoSlime demoSlime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.demoSlime = demoSlime;
    }

    public override void Enter()
    {
        base.Enter();
        demoSlime.setZeroVelocity();
        demoSlime.animator.SetTrigger("Attack_Ready");
    }

    public override void Update()
    {
        base.Update();
        if (triggerCalled)
        {
            stateMachine.changeState(demoSlime._battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        demoSlime.lastTime = Time.time;
    }
}
