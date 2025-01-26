using UnityEngine;

public class WolfAttackState : EnemyState
{
    private Wolf _wolf;
    public WolfAttackState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Wolf wolf) : base(enemyBase, stateMachine, animationBoolName)
    {
        _wolf = wolf;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        _wolf.setZeroVelocity();
        if (triggerCalled)
        {
            stateMachine.changeState(_wolf._battleState);
        }
    }

    public override void FixUpdate()
    {
        base.FixUpdate();
    }

    public override void Exit()
    {
        base.Exit();
        _wolf.lastTime = Time.time;
    }
}
