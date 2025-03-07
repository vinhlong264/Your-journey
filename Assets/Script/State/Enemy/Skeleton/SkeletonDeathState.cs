using UnityEngine;

public class SkeletonDeathState : EnemyState
{
    private Skeleton skeleton;
    public SkeletonDeathState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Skeleton skeleton) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.skeleton = skeleton;
    }

    public override void Enter()
    {
        base.Enter();
        skeleton.rb.bodyType = RigidbodyType2D.Static;
        skeleton.setZeroVelocity();
        skeleton.cd.enabled = false;
        skeleton.DeactiveMe();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
    }
}
