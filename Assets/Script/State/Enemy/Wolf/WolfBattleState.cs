using UnityEngine;

public class WolfBattleState : EnemyState
{
    private Wolf _wolf;
    private Player _player;
    private float moveDir;
    public WolfBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Wolf wolf) : base(enemyBase, stateMachine, animationBoolName)
    {
        this._wolf = wolf;
    }

    public override void Enter()
    {
        base.Enter();
        _player = GameManager.Instance.Player;
        if (_player.isDeath)
        {
            stateMachine.changeState(_wolf._runState);
        }
    }

    public override void Update()
    {
        base.Update();

        BattleAttackHandler();
    }

    public override void FixUpdate()
    {
        _wolf.setVelocity(moveDir, _wolf.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
    private void BattleAttackHandler()
    {
        if (!_wolf.isPlayerDetected())
        {
            if (stateTimer < 0 || Vector2.Distance(_player.transform.position, _wolf.transform.position) > 7)
            {
                Debug.Log("ChangeState Idle");
                stateMachine.changeState(_wolf._idleState);
                return;
            }
        }

        if (_wolf.isPlayerDetected().distance < _wolf.AttackDis)
        {
            stateTimer = _wolf.BattleTime;
            if (canAttack())
            {
                stateMachine.changeState(_wolf._attackState);
                return;
            }
        }

        if (_player.transform.position.x > _wolf.transform.position.x)
        {
            moveDir = 1f;
        }
        else if (_player.transform.position.x < _wolf.transform.position.x)
        {
            moveDir = -1f;
        }
    }
}
