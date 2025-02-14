using UnityEngine;

public class DemoSlime : Enemy
{
    [SerializeField] private Transform attackCheck;

    [Header("Jump state infor")]
    [SerializeField] private Vector2 jumpForce;
    [SerializeField] private float jumpTimeCoolDown;
    [SerializeField] private float rangeSafe;
    public float lastTimeJump { get; set; }

    public Vector2 JumpForce { get => jumpForce; }
    public float JumpTimeCoolDown { get => jumpTimeCoolDown; }
    public float RangeSafe { get => rangeSafe; }

    #region State
    public DemoSlimeIdleState _idleState { get; private set; }
    public DemoSlimeRunState _runState { get; private set; }
    public DemoSlimeBattleAttackState _battleState { get; private set; }
    public DemoSlimeJumpState _jumpState { get; private set; }
    public DemoSlimeAttackState _attackState { get; private set; }
    public DemoSlimeAttackBulletState _attackBulletState { get; private set; }

    #endregion
    protected override void Awake()
    {
        base.Awake();
        _idleState = new DemoSlimeIdleState(this, stateMachine, "IDLE", this);
        _runState = new DemoSlimeRunState(this, stateMachine, "RUN", this);
        _battleState = new DemoSlimeBattleAttackState(this, stateMachine, "RUN", this);
        _jumpState = new DemoSlimeJumpState(this, stateMachine, "IDLE", this);
        _attackState = new DemoSlimeAttackState(this, stateMachine, "ATTACK", this);
        _attackBulletState = new DemoSlimeAttackBulletState(this, stateMachine, "AttackBullet", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.initialize(_idleState);
    }

    protected override void Update()
    {
        stateMachine.currentState.Update();
    }


    public override void dameEffect()
    {
        base.dameEffect();
    }

    public override RaycastHit2D isPlayerDetected()
    {
        return Physics2D.Raycast(attackCheck.position, Vector2.right * isFacingDir, AttackCheckDis, isPlayer);
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(attackCheck.position, new Vector3(attackCheck.position.x + _attackCheckDis * isFacingDir, attackCheck.position.y, attackCheck.position.z));
        Gizmos.DrawWireSphere(_attackArea.position, _attackRadius);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(ground.position, groundCheckDis);
    }
}
