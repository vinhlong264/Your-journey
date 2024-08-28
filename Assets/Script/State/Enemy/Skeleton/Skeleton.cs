using System.Collections;
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


    [Header("Fx")]
    public EntityFx fx;
    #region State
    public SkeletonIdleState idleState {  get; private set; }
    public SkeletonRunState runState { get; private set; }
    public SkeletonBattleState battleState { get; private set; }
    public SkeletonAttackState attackState { get; private set; }
    public SkeletonStunState stunState { get; private set; }

    #endregion

    public override void Awake()
    {
        base.Awake();
        idleState = new SkeletonIdleState(this , StateMachine , "Idle" , this );
        runState = new SkeletonRunState(this , StateMachine, "Run" , this );
        battleState = new SkeletonBattleState(this , StateMachine , "Run" , this);
        attackState = new SkeletonAttackState(this, StateMachine, "Attack", this);
        stunState = new SkeletonStunState(this, StateMachine, "Stun", this);
    }

    public override void Start()
    {
        base.Start();
        StateMachine.initialize(idleState);
        fx = GetComponent<EntityFx>();
        currentHp = hpMax;
        isFacingDir = 1f;
        isFacingRight = true;
    }

    public override void DameEffect()
    {
        //Debug.Log("Receive dame");
        fx.StartCoroutine("FlashFx");
        StartCoroutine("isKnockBack");
    }

    public override void Update()
    {
        base.Update();
        //if (Input.GetKeyDown(KeyCode.U))
        //{
        //    StateMachine.changeState(stunState);
        //}

        //Debug.Log("Stun check: " + checkStunned());
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
}
