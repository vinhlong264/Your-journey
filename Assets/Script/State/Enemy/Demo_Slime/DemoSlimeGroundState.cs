using UnityEngine;

public class DemoSlimeGroundState : EnemyState
{
    protected DemoSlime _demoSlime;
    public DemoSlimeGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName , DemoSlime demoSlime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this._demoSlime = demoSlime;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
    }

    public override void Exit()
    {
        base.Exit();
    }
}
