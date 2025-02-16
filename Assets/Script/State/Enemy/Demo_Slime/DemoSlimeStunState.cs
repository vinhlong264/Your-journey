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
        demoSlime.fx.setTimeDuration(demoSlime.StunDuration);
        demoSlime.fx.StunColorFor();
        stateTimer = demoSlime.StunDuration;
        demoSlime.rb.velocity = new Vector2(demoSlime.StunDirection.x * (-demoSlime.isFacingDir), demoSlime.StunDirection.y);
    }

    public override void Update()
    {
        base.Update();

        if(stateTimer < 0)
        {
            stateMachine.changeState(demoSlime._idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
