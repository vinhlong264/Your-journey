
using UnityEngine;

public class EnemyState
{
    protected Enemy enemyBase {  get; private set; }
    protected EnemyStateMachine stateMachine { get; private set; }
    protected bool triggerCalled;
    protected float stateTimer;

    private string animationBoolName;

    public EnemyState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName)
    {
        this.enemyBase = enemyBase;
        this.stateMachine = stateMachine;
        this.animationBoolName = animationBoolName;
    }

    public virtual void Enter()
    {
        //Debug.Log("I doing enter " + this.animationBoolName);
        triggerCalled = false;
        enemyBase.animator.SetBool(animationBoolName, true);
    }

    public virtual void Update()
    {
        stateTimer -= Time.deltaTime;
    }

    public virtual void Exit() 
    {
        //Debug.Log("I doing exit " + this.animationBoolName);
        enemyBase.animator.SetBool(animationBoolName , false);
    }

    public virtual void AnimationTriggerCalled()
    {
        triggerCalled = true;
    }
    
}
