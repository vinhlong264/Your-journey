using UnityEngine;

public abstract class EnemyState
{
    protected Enemy enemyBase;
    protected EnemyStateMachine stateMachine;
    protected bool triggerCalled;
    protected float stateTimer;

    protected string animationBoolName;

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

    public virtual void FixUpdate()
    {

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

    protected bool canAttack()
    {
        if (Time.time >= enemyBase.lastTime + enemyBase.AttackCoolDown)
            //Kiểm tra nếu Time.time >= lastTime + attackCooldown sẽ gán lastTime = Time.time và trả hàm này về true, và ngược lại
        {
            enemyBase.lastTime = Time.time;
            return true;
        }
        return false;
    }

}
