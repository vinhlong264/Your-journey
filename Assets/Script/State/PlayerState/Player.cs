using System.Collections;
using UnityEditor;
using UnityEngine;

public class Player : Entity
{
    #region variable
    public float jumpForce;

    [Header("Default value")]
    private float deafaultMoveSpeed;
    private float deadaultJumpForce;
    private float deadaultDashSpeed;
    
    [Header("Attack info")]
    public float[] attackMovement;
    public Transform AttackCheck;
    public float attackRadius;
    public float counterAttackDurarion; // thời gian trong state counter
    public bool isBusy { get; private set; }

    [Header("Dash Info")]
    public float dashSpeed;
    public float dashDirection { get; private set; }
    public float dashDuration;
    public float dashTimer { get; set; }
    [SerializeField] float dashCooldown;

    public SkillManager skill { get; private set; }

    public GameObject sword;

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
        _stateMachine.initialize(_idleState);

        skill = SkillManager.instance;

        isFacingRight = true;
        isFacingDir = 1f;


        deafaultMoveSpeed = moveSpeed;
        deadaultJumpForce = jumpForce;
        deadaultDashSpeed = dashSpeed;
    }

    // Update is called once per frame
    public override void Update()
    {
        _stateMachine.currentState.Execute(); //Thực hiện việc chạy các state
        checkForDashInput();

        if (Input.GetKeyDown(KeyCode.F) && skill.crystal_skill.crystalSkillUnlocked)
        {
            skill.crystal_skill.CanUseSkill();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Inventory.Instance.useCanBottle();
        }
    }

    public override void slowEntityBy(float _slowPercentage, float _slowDuration)
    {
        moveSpeed = moveSpeed * (1f -  _slowPercentage);
        jumpForce = jumpForce * (1f - _slowPercentage);
        dashSpeed = dashSpeed * (1f - _slowPercentage);
        animator.speed = animator.speed * (1f - _slowPercentage);

        Invoke("returnDefaultValue", _slowDuration);
    }


    protected override void returnDefaultValue()
    {
        base.returnDefaultValue();
        moveSpeed = deafaultMoveSpeed;
        jumpForce = deadaultJumpForce;
        dashSpeed = deadaultDashSpeed;
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

        if (!skill.dash_skill.dashUnlock)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) && skill.dash_skill.CanUseSkill()) // kích hoạt dash
        {

            dashDirection = Input.GetAxisRaw("Horizontal");

            if (dashDirection == 0)
            {
                dashDirection = isFacingDir; // nếu dashDirection == 0 thì gán dashDirection cho hướng nhìn của hiện tại
            }

            _stateMachine.changeState(_dashState);
        }
    }

    public override void dameEffect()
    {
        fx.StartCoroutine("FlashFx");
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
