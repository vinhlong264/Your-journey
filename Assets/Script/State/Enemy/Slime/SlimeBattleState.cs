using System.Security.Cryptography;
using UnityEngine;
public class SlimeBattleState : EnemyState
{
    private Slime _slime;
    private int countAtack;
    private Player _player;
    private float moveDir;
    public SlimeBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Slime slime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this._slime = slime;
    }

    public override void Enter()
    {
        base.Enter();
        _player = GameManager.Instance.player;
        countAtack++;
        if (countAtack > 7)
        {
            _slime.activeSkill();
            countAtack = 0;
        }
    }

    public override void Update()
    {
        base.Update();
        if (_slime.isPlayerDetected())
        {
            if (_slime.isPlayerDetected().distance < _slime.attackDistance)
            {
                stateTimer = _slime.BattleTime;
                if (canAttack())
                {
                    stateMachine.changeState(_slime.attackState);
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(_player.transform.position , _slime.transform.position) < 7)
            {
                stateMachine.changeState(_slime.idleState);
            }
        }

        if(_player.transform.position.x > _slime.transform.position.x)
        {
            moveDir = 1f;
        }
        else if(_player.transform.position.x < _slime.transform.position.x)
        {
            moveDir = -1f;
        }
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
        _slime.setVelocity(moveDir, _slime.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    private bool canAttack()
    {
        if (Time.time >= _slime.lastTime + _slime.attackCoolDown)
        {
            _slime.lastTime = Time.time;
            return true;
        }

        return false;
    }
}
