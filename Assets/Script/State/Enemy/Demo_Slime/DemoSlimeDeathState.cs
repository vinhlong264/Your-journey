using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSlimeDeathState : EnemyState
{
    private DemoSlime demoSlime;
    public DemoSlimeDeathState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, DemoSlime demoSlime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.demoSlime = demoSlime;
    }

    public override void Enter()
    {
        base.Enter();
        demoSlime.rb.bodyType = RigidbodyType2D.Static;
        demoSlime.cd.enabled = false;
        demoSlime.setZeroVelocity();
        demoSlime.DeactiveMe();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
