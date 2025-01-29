public class EnemyIdleStateBase : EnemyGroundStateBase
{
    public EnemyIdleStateBase(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName) : base(enemyBase, stateMachine, animationBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = 1f;
    }

    public override void Update()
    {
        base.Update();
        if(stateTimer < 0f)
        {
            stateMachine.changeState(enemyBase._runStateBase);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
