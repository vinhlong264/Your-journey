using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStunStateBase : EnemyState
{
    public EnemyStunStateBase(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName) : base(enemyBase, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        enemyBase.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);
        stateTimer = enemyBase.StunDuration;
        enemyBase.setVelocity(enemyBase.isFacingDir * enemyBase.StunDirection.x , enemyBase.StunDirection.y);
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0f)
        {
            stateMachine.changeState(enemyBase._idleStateBase);
            enemyBase.isCanStun = true;
        }
    }

    public override void Exit()
    {
        base.Exit();
        enemyBase.fx.Invoke("canCelRedBlink", 0);
    }
}
