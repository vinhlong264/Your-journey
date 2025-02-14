using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Skeleton enemy;
    private Player Player;
    private float moveDir;
    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName , Skeleton skeleton) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.enemy = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        Player = GameManager.Instance.player;
        if (Player.isDeath)
        {
            stateMachine.changeState(enemy.runState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (!enemy.isPlayerDetected())
        {
            if (stateTimer < 0 || Vector2.Distance(Player.transform.position, enemy.transform.position) > 7)
            {
                stateMachine.changeState(enemy.idleState);
                return;
            }
        }


        if (enemy.isPlayerDetected().distance < enemy.AttackDis)
        {
            stateTimer = enemy.BattleTime;
            if (canAttack())
            {
                stateMachine.changeState(enemy.attackState);
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
