using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSlimeAttackBulletState : EnemyState
{
    private DemoSlime demoSlime;
    public DemoSlimeAttackBulletState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, DemoSlime demoSlime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.demoSlime = demoSlime;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (triggerCalled)
        {
            stateMachine.changeState(demoSlime._battleState);
        }
    }
}
