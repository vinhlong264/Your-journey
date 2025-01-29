using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBattleStateBase : EnemyState
{
    protected Player _player;
    protected float moveAttackDir;
    public EnemyBattleStateBase(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName) : base(enemyBase, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        _player = GameManager.Instance.player;

    }

    public override void Update()
    {
        base.Update();
        Battle();
    }

    public override void Exit()
    {
        base.Exit();
    }

    protected void Battle()
    {
        if (!enemyBase.isPlayerDetected())
        {
            if(stateTimer < 0f || Vector2.Distance(enemyBase.transform.position , _player.transform.position) < 7f)
            {
                stateMachine.changeState(enemyBase._idleStateBase);
                return;
            }
        }

        if (enemyBase.isPlayerDetected().distance < enemyBase.AttackDis)
        {
            stateTimer = enemyBase.BattleTime;
            if (canAttack())
            {
                stateMachine.changeState(enemyBase._attackStateBase);
            }
        }

        if(_player.transform.position.x > enemyBase.transform.position.x)
        {
            moveAttackDir = 1f;
        }
        else if(_player.transform.position.x < enemyBase.transform.position.x)
        {
            moveAttackDir = -1f;
        }

        enemyBase.setVelocity(moveAttackDir , enemyBase.rb.velocity.y);
    }
}
