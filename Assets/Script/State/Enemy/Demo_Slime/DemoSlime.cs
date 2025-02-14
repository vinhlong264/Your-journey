using System.Collections.Generic;
using UnityEngine;

public class DemoSlime : Enemy
{
    [SerializeField] private Transform attackCheck;

    [Header("Jump state infor")]
    [SerializeField] private Vector2 jumpForce;
    [SerializeField] private float jumpTimeCoolDown;
    [SerializeField] private float rangeSafe;
    private List<EnemyState> listAttackState;

    [Header("Spell state infor")]
    [SerializeField] private Collider2D boundSpawn;
    [SerializeField] private float spellTime;
    [SerializeField] private GameObject fireFall;


    #region Get set
    //Get set: Jump State
    public float lastTimeJump { get; set; }
    public Vector2 JumpForce { get => jumpForce; }
    public float JumpTimeCoolDown { get => jumpTimeCoolDown; }
    public float RangeSafe { get => rangeSafe; }

    public Collider2D BoundSpawn { get => boundSpawn; }
    public float SpellTime { get => spellTime; }
    public GameObject FireFall { get => fireFall; }
    #endregion

    #region State
    public DemoSlimeIdleState _idleState { get; private set; }
    public DemoSlimeRunState _runState { get; private set; }
    public DemoSlimeBattleAttackState _battleState { get; private set; }
    public DemoSlimeJumpState _jumpState { get; private set; }
    public DemoSlimeAttackState _attackState { get; private set; }
    public DemoSlimeAttackBulletState _attackBulletState { get; private set; }
    public DemoSlimeSpellState _spellState { get ; private set; }

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
        _spellState = new DemoSlimeSpellState(this, stateMachine, "SPELL", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.initialize(_idleState);
        listAttackState = new List<EnemyState>()
        {
            _attackState,
            _attackBulletState
        };
    }

    protected override void Update()
    {
        stateMachine.currentState.Update();
    }


    public override void dameEffect()
    {
        base.dameEffect();
    }

    public List<EnemyState> GetLisAttackState()
    {
        if(listAttackState.Count == 0) return null;

        return listAttackState;
    }

    public void initializeFireFall()
    {
        Vector2 Pos = new Vector2(Random.Range(-3f, 14), 18);
        GameObject dump = GameManager.Instance.GetObjFromPool(fireFall);
        dump.transform.position = Pos;
        dump.transform.rotation = Quaternion.Euler(0, 0, -90);
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
