using UnityEngine;

public class SkeletonBattleState : EnemyBattleStateBase
{
    private Skeleton enemy;
    private Player Player;
    private float moveDir;
    public SkeletonBattleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName) : base(enemyBase, stateMachine, animationBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();
        //Player = GameManager.Instance.player;
        //if (Player.isDeath)
        //{
        //    stateMachine.changeState(enemy.runState);
        //}
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
      
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }
}
