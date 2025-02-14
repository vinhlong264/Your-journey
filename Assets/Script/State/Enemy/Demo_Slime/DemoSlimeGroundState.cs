using UnityEngine;

public class DemoSlimeGroundState : EnemyState
{
    protected DemoSlime demoSlime;
    public DemoSlimeGroundState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, DemoSlime demoSlime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.demoSlime = demoSlime;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        if (demoSlime.isPlayerDetected())
        {
            Debug.Log("Change state battle");
            stateMachine.changeState(demoSlime._battleState);
        }
    }

    

    public override void Exit()
    {
        base.Exit();
    }
}
