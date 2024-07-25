using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGroundState : EnemyState
{
    protected Enemy_Slime slime;
    public SlimeGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Enemy_Slime slime) : base(enemyBase, stateMachine, animationBoolName)
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
    }

    public override void Update()
    {
        base.Update();
        if (slime.isPlayerDetected())
        {
            stateMachine.changeState(slime.battleState);
        }
    }
}
