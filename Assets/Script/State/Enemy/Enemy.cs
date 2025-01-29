using System.Collections;
using UnityEngine;

public abstract class Enemy : Entity
{
    [Header("Attack info")]
    [SerializeField] protected float battleTime;
    [SerializeField] protected float attackCoolDown;
    public float lastTime;

    [Space]
    [SerializeField] protected Transform _attackArea;
    [SerializeField] protected float _attackDis;
    [SerializeField] protected float _attackCheckDis;
    [SerializeField] protected float _attackRadius;
    [SerializeField] protected LayerMask isPlayer;


    [Header("Stun info")]
    [SerializeField] protected float stunDuration;
    [SerializeField] protected Vector2 stunDirection;
    public bool isCanStun { get; set; } = true;
    [SerializeField] protected GameObject AttackImage;
    private float defaultSpeed;

    #region State Base
    public EnemyStateMachine stateMachine {  get; private set; }
    public EnemyIdleStateBase _idleStateBase { get; private set; }
    public EnemyRunStateBase _runStateBase { get; private set; }
    public EnemyBattleStateBase _battleAttackBase { get; private set; }
    public EnemyAttackStateBase _attackStateBase { get; private set; }
    public EnemyStunStateBase _stunStateBase { get; private set; }
    public EnemyDeathStateBase _deathStateBase { get; private set; }
    #endregion


    #region Get Set

    //Attack Variable
    public float BattleTime { get => battleTime; }

    public float AttackCoolDown { get => attackCoolDown; }
    public float AttackRadius { get => _attackRadius; }
    public float AttackCheckDis {  get => _attackCheckDis; }
    public float AttackDis { get => _attackDis; }

    public Transform AttackArea { get => _attackArea; }


    //Stun variable
    public float StunDuration { get => stunDuration; }
    public Vector2 StunDirection { get => stunDirection; }

    #endregion

    protected override void Awake()
    {
        stateMachine = new EnemyStateMachine();
        _idleStateBase = new EnemyIdleStateBase(this, stateMachine , Constant.Animation_IDLE);
        _runStateBase = new EnemyRunStateBase(this , stateMachine , Constant.Animation_RUN);
        _battleAttackBase = new EnemyBattleStateBase(this , stateMachine, Constant.Animation_RUN);
        _attackStateBase = new EnemyAttackStateBase(this , stateMachine , Constant.Animation_ATTACK);
        _stunStateBase = new EnemyStunStateBase(this , stateMachine , Constant.Animation_STUN);
        _deathStateBase = new EnemyDeathStateBase(this , stateMachine , Constant.Animation_DEATH);

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.initialize(_idleStateBase);
        defaultSpeed = moveSpeed;
    }
    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        stateMachine.currentState.FixUpdate();
    }

    #region Conuter attack
    public virtual bool checkStunned()
    {
        if (isCanStun) 
        {
            //CloseCounterAttack();
            isCanStun = false;
            return true;
        }
        return false;
    }

    public void OpenAttackWindow()
    {
        AttackImage.SetActive(true);
    }

    public void CloseAttackWindow()
    {
        AttackImage.SetActive(false);
    }

    #endregion

    public virtual void FreezeToTimer(bool _timeFrozen) // Đóng băng hoạt động của Enemy lại khi bị tấn công bởi các skill của Player
    {
        if (_timeFrozen)
        {
            moveSpeed = 0;
            animator.speed = 0;
        }
        else
        {
            moveSpeed = defaultSpeed;
            animator.speed = 1;
        }
    }

    public void DeactiveMe()
    {
        StartCoroutine(DeactiveMeCrountine());
    }

    IEnumerator DeactiveMeCrountine()
    {
        yield return new WaitForSeconds(3f);
        gameObject.SetActive(false);
    }


    public virtual void FreezeBy(float _time) => StartCoroutine(FreezeForTimer(_time));

    protected virtual IEnumerator FreezeForTimer(float _second)
    {
        Debug.Log("start Frezee time");
        FreezeToTimer(true);
        yield return new WaitForSeconds(_second);
        FreezeToTimer(false);
        Debug.Log("end Frezee time");
    }

    protected virtual IEnumerator BleedingHealth(float _second)
    {
        status.applyBleedingHealth(true);
        Debug.Log("Đang rỉ máu");
        yield return new WaitForSeconds(_second);
        Debug.Log("Ngừng rỉ máu");
        status.applyBleedingHealth(false);
    }


    public override void dameEffect()
    {
        base.dameEffect();
    }

    public override void Die()
    {
        stateMachine.changeState(_deathStateBase);
    }

    public void animationTriggerFinish() => stateMachine.currentState.AnimationTriggerCalled();

    public virtual RaycastHit2D isPlayerDetected() => Physics2D.Raycast(transform.position, Vector2.right * isFacingDir, _attackCheckDis, isPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _attackCheckDis * isFacingDir, transform.position.y));

        if (_attackArea == null) return;
        Gizmos.DrawWireSphere(_attackArea.position, _attackRadius);
    }
}
