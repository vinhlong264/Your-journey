﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    public float attackDistance;
    public float attackCooldown;
    public float lastTime;

    [Header("Health infor")]
    public float hpMax;
    public float currentHp;

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
        idleState = new SkeletonIdleState(this , StateMachine , "Idle" , this );
        runState = new SkeletonRunState(this , StateMachine, "Run" , this );
        battleState = new SkeletonBattleState(this , StateMachine , "Run" , this);
        attackState = new SkeletonAttackState(this, StateMachine, "Attack", this);
        stunState = new SkeletonStunState(this, StateMachine, "Stun", this);
        deathState = new SkeletonDeathState(this, StateMachine, "Death", this);
    }

    protected override void Start()
    {
        base.Start();
        StateMachine.initialize(idleState);
        currentHp = hpMax;
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
}
