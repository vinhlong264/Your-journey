
using UnityEngine;

public class SkeletonStunState : EnemyStunStateBase
{
    private Skeleton enemy;
    public SkeletonStunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName) : base(enemyBase, stateMachine, animationBoolName)
    {
       
    }

    public override void Enter()
    {
        base.Enter();
        //enemy.fx.InvokeRepeating("RedColorBlink", 0, 0.1f);
        ///*InvokeRepeating(method name, time , repeatTime): là một method được gọi theo giây và sau đó lập lại ở mỗi giây
        // * time: thời gian được gọi
        // * repeatTime: thời gian lặp lại mỗi giây
        // */
        //stateTimer = enemy.StunDuration;
        //enemy.rb.velocity = new Vector2(-enemy.isFacingDir * enemy.StunDirection.x , enemy.StunDirection.y );
    }

    public override void Exit()
    {
        base.Exit();
        //enemy.fx.Invoke("canCelRedBlink", 0);
    }

    public override void Update()
    {
        base.Update();
        //if(stateTimer < 0)
        //{
        //    stateMachine.changeState(enemy.idleState);
        //    enemyBase.isCanStun = true;
        //}
    }
}
