using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfStunState : EnemyState
{
    private Wolf _wolf;

    public WolfStunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Wolf wolf) : base(enemyBase, stateMachine, animationBoolName)
    {
        this._wolf = wolf;
    }

    public override void Enter()
    {
        base.Enter();
        _wolf.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);
        stateTimer = _wolf.StunDuration;
        _wolf.setVelocity(_wolf.StunDirection.x * -_wolf.isFacingDir, _wolf.StunDirection.y);
    }


    public override void Update()
    {
        base.Update();
        if(stateTimer < 0)
        {
            stateMachine.changeState(_wolf._idleState);
            _wolf.isCanStun = true;
        }
    }
    public override void Exit()
    {
        base.Exit();
        _wolf.fx.Invoke("canCelRedBlink", 0);
    }
}
