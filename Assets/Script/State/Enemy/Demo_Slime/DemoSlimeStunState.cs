using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSlimeStunState : EnemyState
{
    private DemoSlime demoSlime;
    public DemoSlimeStunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, DemoSlime demoSlime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.demoSlime = demoSlime;
    }

    public override void Enter()
    {
        base.Enter();
        
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
