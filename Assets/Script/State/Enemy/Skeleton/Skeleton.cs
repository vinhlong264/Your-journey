using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    #region State
    public SkeletonIdleState idleState {  get; private set; }
    public SkeletonRunState runState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunState stunState { get; private set; }
    public SkeletonDeathState deathState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();
        //idleState = new SkeletonIdleState(this , stateMachine , "Idle" , this );
        //runState = new SkeletonRunState(this , stateMachine, "Run" , this );
        //battleState = new SkeletonBattleState(this , stateMachine , "Run" , this);
        //attackState = new SkeletonAttackState(this, stateMachine, "Attack", this);
        //stunState = new SkeletonStunState(this, stateMachine, "Stun", this);
        //deathState = new SkeletonDeathState(this, stateMachine, "Death", this);
    }

    protected override void Start()
    {
        base.Start();
        //stateMachine.initialize(idleState);
        isFacingDir = 1f;
        isFacingRight = true;
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public override void dameEffect()
    {
        base.dameEffect();
    }


    public override bool checkStunned()
    {
        if (base.checkStunned())
        {
            stateMachine.changeState(stunState);
            return true;
        }
        return false;
    }


    public override void Die()
    {
        base.Die();
        stateMachine.changeState(deathState);
    }
}
