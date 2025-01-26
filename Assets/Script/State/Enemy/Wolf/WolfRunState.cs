using UnityEngine;
public class WolfRunState : WolfGroundState
{
    public WolfRunState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Wolf wolf) : base(enemyBase, stateMachine, animationBoolName, wolf)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (!_wolf.groundCheck() || _wolf.wallCheck())
        {
            Debug.Log("ChangState");
            _wolf.Flip();
            stateMachine.changeState(_wolf._idleState);
        }
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
        _wolf.setVelocity(_wolf.moveSpeed * _wolf.isFacingDir, _wolf.rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }
}
