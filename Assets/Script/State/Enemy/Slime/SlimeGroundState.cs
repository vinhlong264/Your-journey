using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeGroundState : EnemyState
{
    protected Slime _slime;
    public SlimeGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName , Slime slime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this._slime = slime;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (_slime.isPlayerDetected())
        {
            stateMachine.changeState(_slime.battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
