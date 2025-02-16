
using UnityEngine;

public class SkeletonStunState : EnemyState
{
    private Skeleton enemy;
    public SkeletonStunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName , Skeleton skeleton) : base(enemyBase, stateMachine, animationBoolName)
    {
       this.enemy = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.setTimeDuration(enemy.StunDuration);
        enemy.fx.StunColorFor();
        stateTimer = enemy.StunDuration;
        enemy.rb.velocity = new Vector2(-enemy.isFacingDir * enemy.StunDirection.x, enemy.StunDirection.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.changeState(enemy.idleState);
            enemyBase.isCanStun = true;
        }
    }
}
