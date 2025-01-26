using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfGroundState : EnemyState
{
    protected Wolf _wolf;
    public WolfGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Wolf wolf) : base(enemyBase, stateMachine, animationBoolName)
    {
        this._wolf = wolf;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (_wolf.isPlayerDetected())
        {
            stateMachine.changeState(_wolf._battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
