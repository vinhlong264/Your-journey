using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : EnemyState
{
    private Enemy_Slime slime;

    public SlimeAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName , Enemy_Slime slime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.slime = slime;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
        slime.Lastime = Time.time;
    }

    public override void Update()
    {
        base.Update();
        slime.setZeroVelocity();
        if (triggerCalled)
        {
            stateMachine.changeState(slime.idleState);
        }
    }
}
