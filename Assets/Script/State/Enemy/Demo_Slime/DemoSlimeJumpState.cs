using UnityEngine;

public class DemoSlimeJumpState : EnemyState
{
    private DemoSlime demoSlime;

    public DemoSlimeJumpState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, DemoSlime demoSlime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.demoSlime = demoSlime;
    }

    public override void Enter()
    {
        base.Enter();
        demoSlime.rb.velocity = new Vector2(demoSlime.JumpForce.x * (-demoSlime.isFacingDir), demoSlime.JumpForce.y);
    }

    public override void Update()
    {
        base.Update();
        if (demoSlime.rb.velocity.y < 0 && demoSlime.groundCheck())
        {
            stateMachine.changeState(demoSlime._idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
    }
}
