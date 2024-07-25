

public class SlimeRunState : SlimeGroundState
{
    public SlimeRunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Enemy_Slime slime) : base(enemyBase, stateMachine, animationBoolName, slime)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        slime.setVelocity(slime.moveSpeed * slime.isFacingDir, slime.rb.velocity.y);
        if (!slime.groundCheck() || slime.wallCheck())
        {
            slime.Flip();
            stateMachine.changeState(slime.idleState);
        }

    }
}
