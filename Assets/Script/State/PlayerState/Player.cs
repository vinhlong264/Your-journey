using System.Collections;
using UnityEngine;

public class Player : Entity
{
    public float jumpForce;
    [Header("Health infor")]
    [SerializeField] float maxHp;
    private float currentHp;
    
    [Header("Attack info")]
    public float[] attackMovement;
    public Transform AttackCheck;
    public float attackRadius;
    public float counterAttackDurarion;
    public bool isBusy { get; private set; }

    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDirection { get; private set; }
    public float dashDuration;
    public float dashTimer { get; set; }
    [SerializeField] float dashCooldown;

    public GameObject sword;

    #region Component
    private EntityFx entityFx;

    #endregion

    #region State
    public PlayerStateMachine _stateMachine { get; private set; }
    public PlayerIdleState _idleState { get; private set; }
    public PlayerRunState _runState { get; private set; }
    public PlayerJumpState _jumpState { get; private set; }
    public PlayerAirState _airState { get; private set; }
    public PlayerDashState _dashState { get; private set; }
    public PlayerWallSlideState _wallSlideState { get; private set; }
    public PlayerJumpWallState _jumpWallState { get; private set; }
    public PlayerAttackState _attackState { get; private set; }
    public PlayerCounterAttackState _counterAttackState { get; private set; }
    public PlayerAnimSwordState _animSwordState { get; private set; }
    public PlayerCatchSwordState _catchSwordState { get; private set; }
    public PlayerBlackHoleState _blackHoleState { get; private set; }
    public PlayerDeathState _deathState { get; private set; }

    #endregion


    public override void Awake()
    {
        _stateMachine = new PlayerStateMachine();
        /*Khởi tạo các đối tượng của các State:
         *  +) this: đối tượng chứa script này
         *  +) _stateMachine: Quản lý các state
         *  +) string animationBool: tên các state(hay animtion) được truyền vào
        */
        _idleState = new PlayerIdleState(this, _stateMachine, "Idle");
        _runState = new PlayerRunState(this, _stateMachine, "Run");
        _jumpState = new PlayerJumpState(this, _stateMachine, "Jump");
        _airState = new PlayerAirState(this, _stateMachine, "Jump");
        _jumpWallState = new PlayerJumpWallState(this, _stateMachine, "Jump");
        _dashState = new PlayerDashState(this, _stateMachine, "Dash");
        _wallSlideState = new PlayerWallSlideState(this, _stateMachine, "WallSlide");
        _attackState = new PlayerAttackState(this, _stateMachine, "Attack");
        _counterAttackState = new PlayerCounterAttackState(this, _stateMachine, "CounterAttack");
        _animSwordState = new PlayerAnimSwordState(this, _stateMachine, "animSword");
        _catchSwordState = new PlayerCatchSwordState(this, _stateMachine, "catchSword");
        _blackHoleState = new PlayerBlackHoleState(this, _stateMachine, "Jump");
        _deathState = new PlayerDeathState(this, _stateMachine, "Death");
    }

    public override void Start()
    {
        base.Start();
        entityFx = GetComponent<EntityFx>();
        _stateMachine.initialize(_idleState);
        currentHp = maxHp;
        isFacingRight = true;
        isFacingDir = 1f;
    }

    // Update is called once per frame
    public override void Update()
    {
        _stateMachine.currentState.Execute(); //Thực hiện việc chạy các state
        checkForDashInput();

        if (Input.GetKeyDown(KeyCode.F))
        {
            SkillManager.instance.crystal_skill.CanUseSkill();
        }
    }

    #region Skill
    public void AsignNewSword(GameObject newSword)
    {
        sword = newSword;
    }


    public void CatchTheSword() // chuyển về State Catch sword
    {
        _stateMachine.changeState(_catchSwordState);
        Destroy(sword);
    }


    public void ExitStateBlackHole()
    {
        Debug.Log("Thoát khỏi trạng thái Black Hole");
        _stateMachine.changeState(_airState);
    }
    #endregion

    public IEnumerator isBusyFor(float second) // Croutine check Player có đang tấn công không. Khi kích hoạt mới log
    {
        isBusy = true;
        yield return new WaitForSeconds(second);
        isBusy = false;
    }

    public void AnimationEventTrigger()
    {
        _stateMachine.currentState.animationTriggerEvent();
    }


    private void checkForDashInput()
    {
        if (wallCheck())
        {
            return;
        }
        //dashTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash_skill.CanUseSkill()) // kích hoạt dash
        {
            /*dashTimer = dashCooldown;*/ // sau khi kích hoạt dash sẽ chờ 1 khoảng delay để kích hoạt được tiếp
            dashDirection = Input.GetAxisRaw("Horizontal");

            if (dashDirection == 0)
            {
                dashDirection = isFacingDir; // nếu dashDirection == 0 thì gán dashDirection cho hướng nhìn của hiện tại
            }

            _stateMachine.changeState(_dashState);
        }
    }

    public void DameEffect()
    {
        entityFx.StartCoroutine("FlashFx");
        //Debug.Log("Attack: " + gameObject.name);
    }

    public override void Die()
    {
        _stateMachine.changeState(_deathState);
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(AttackCheck.position, attackRadius);
    }
}
