public class DemoSlimeRunState : DemoSlimeGroundState
{
    public DemoSlimeRunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, DemoSlime demoSlime) : base(enemyBase, stateMachine, animationBoolName, demoSlime)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }


    public override void Update()
    {
        base.Update();
        demoSlime.setVelocity(demoSlime.moveSpeed * demoSlime.isFacingDir, demoSlime.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
