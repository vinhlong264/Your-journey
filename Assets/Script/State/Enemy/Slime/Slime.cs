using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slime : Enemy
{
    public float lastTime;
    public float attackCoolDown;
    public float attackDistance;



    public SlimeIdleState idleState;
    public SlimeRunState runState;
    public SlimeBattleState battleState;
    public SlimeAttackState attackState;
    public SlimeStunState stunState;
    public SlimeDeathState deathState;
    protected override void Awake()
    {
        base.Awake();
        idleState = new SlimeIdleState(this, StateMachine, "IDLE" , this);
        runState = new SlimeRunState(this, StateMachine, "RUN" , this);
        battleState = new SlimeBattleState(this , StateMachine , "RUN" , this);
        attackState = new SlimeAttackState(this , StateMachine , "ATTACK" , this);
        stunState = new SlimeStunState(this , StateMachine, "STUN" , this);
        deathState = new SlimeDeathState(this , StateMachine , "DEATH" , this);

    }

    protected override void Start()
    {
        base.Start();
        StateMachine.initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        StateMachine.currentState.FixUpdate();
    }

    public override bool checkStunned()
    {
        if (base.checkStunned())
        {
            StateMachine.changeState(stunState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();
        StateMachine.changeState(deathState);
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
