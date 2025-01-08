using UnityEngine;

public class SlimeDeathState : EnemyState
{
    private Slime _slime;
    public SlimeDeathState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Slime slime) : base(enemyBase, stateMachine, animationBoolName)
    {
        _slime = slime;
    }

    public override void Enter()
    {
        base.Enter();
        _slime.rb.bodyType = RigidbodyType2D.Static;
        _slime.setZeroVelocity();
        _slime.cd.enabled = false;
    }

    public override void Exit()
    {
        base.Exit();
    }
}
