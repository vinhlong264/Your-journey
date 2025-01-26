using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Skeleton enemy;
    private Transform Player;
    private float moveDir;
    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Skeleton enemy) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.enemy = enemy;
    }

    public override void Enter()
    {
        base.Enter();
        Player = GameManager.Instance.player.transform;
        //Debug.Log(Player);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (enemy.isPlayerDetected())
        {
            if (enemy.isPlayerDetected().distance < enemy.AttackDis)
            {
                stateTimer = enemy.BattleTime;
                if (canAttack())
                {
                    stateMachine.changeState(enemy.attackState);
                }
            }
        }
        else
        {
            if (stateTimer < 0 || Vector2.Distance(Player.transform.position, enemy.transform.position) < 7)
            {
                stateMachine.changeState(enemy.idleState);
            }
        }


        if (Player.transform.position.x > enemy.transform.position.x)
        {
            moveDir = 1f;
        }
        else if (Player.transform.position.x < enemy.transform.position.x)
        {
            moveDir = -1f;
        }
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
        enemy.setVelocity(moveDir, enemy.rb.velocity.y);
    }
}
