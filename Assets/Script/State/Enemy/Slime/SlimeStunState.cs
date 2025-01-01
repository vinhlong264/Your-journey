using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStunState : EnemyState
{
    private Slime _slime;
    public SlimeStunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName , Slime slime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this._slime = slime;
    }

    public override void Enter()
    {
        base.Enter();
        _slime.fx.InvokeRepeating("RedColorBlink", 0 , 0.1f);
        stateTimer = _slime.StunDuration;
        _slime.setVelocity(_slime.isFacingDir * _slime.StunDirection.x, _slime.StunDirection.y); 
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            stateMachine.changeState(_slime.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        _slime.fx.Invoke("canCelRedBlink", 0);
    }
}
