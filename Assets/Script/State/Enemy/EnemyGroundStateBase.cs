public class EnemyGroundStateBase : EnemyState
{
    public EnemyGroundStateBase(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName) : base(enemyBase, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (enemyBase.isPlayerDetected())
        {
            stateMachine.changeState(enemyBase._attackStateBase);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
