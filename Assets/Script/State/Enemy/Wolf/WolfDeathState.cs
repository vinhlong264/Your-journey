using UnityEngine;

public class WolfDeathState : EnemyState
{
    private Wolf _wolf;
    public WolfDeathState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, Wolf wolf) : base(enemyBase, stateMachine, animationBoolName)
    {
        this._wolf = wolf;
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
