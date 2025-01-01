using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeAttackState : EnemyState
{
    private Slime _slime;
    public SlimeAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName , Slime slime) : base(enemyBase, stateMachine, animationBoolName)
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
        if (triggerCalled)
        {
            stateMachine.changeState(_slime.battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _slime.setZeroVelocity();
    }
}
