using UnityEngine;

public class SkeletonBattleState : EnemyState
{
    private Skeleton enemy;
    private Player player;
    private float moveDir;
    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName , Skeleton skeleton) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.enemy = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        player = GameManager.Instance.Player;
        if (player.isDeath)
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
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)
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


        if (player.transform.position.x > enemy.transform.position.x)
        {
            moveDir = 1f;
        } 
        else if (player.transform.position.x < enemy.transform.position.x)
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
