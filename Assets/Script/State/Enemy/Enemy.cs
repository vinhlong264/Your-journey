using System.Collections;
using UnityEngine;

public abstract class Enemy : Entity
{
    [Header("Attack info")]
    [SerializeField] protected float battleTime;
    [SerializeField] protected Transform _attackCheck;
    [SerializeField] protected float _attackCheckDis;
    [SerializeField] protected float _attackRadius;
    [SerializeField] protected LayerMask isPlayer;


    [Header("Stun info")]
    [SerializeField] protected float stunDuration;
    [SerializeField] protected Vector2 stunDirection;
    [SerializeField] protected bool isCanStun;
    [SerializeField] protected GameObject CounterImage;


    public float DefaultSpeed;

    #region Get Set

    //Attack Variable
    public float BattleTime { get => battleTime; }
    public float AttackRadius { get => _attackRadius; }
    public float AttackCheckDis {  get => _attackCheckDis; }

    public Transform AttackChecks { get => _attackCheck; }


    //Stun variable
    public float StunDuration { get => stunDuration; }
    public Vector2 StunDirection { get => stunDirection; }

    #endregion

    public EnemyStateMachine StateMachine {  get; private set; }
    protected override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine();
    }

    protected override void Start()
    {
        base.Start();
        DefaultSpeed = moveSpeed;
    }
    protected override void Update()
    {
        base.Update();
        StateMachine.currentState.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
        StateMachine.currentState.FixUpdate();
    }

    #region Conuter attack
    public virtual bool checkStunned()
    {
        if (isCanStun) // isCanStun == true thì sẽ gọi hàm closeCounterAttack() để tắt phát hiện tấn công đi rồi trả hàm này về true                       
                        // ngược lại sẽ trả về false
        {
            CloseCounterAttack();
            return true;
        }
        return false;
    }

    public void OpenCounterAttack()
    {
        isCanStun = true;
        CounterImage.SetActive(true);
    }

    public void CloseCounterAttack()
    {
        isCanStun = false;
        CounterImage.SetActive(false);
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
            moveSpeed = DefaultSpeed;
            animator.speed = 1;
        }
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
        base.Die();
    }

    public void animationTriggerFinish() => StateMachine.currentState.AnimationTriggerCalled();

    public RaycastHit2D isPlayerDetected() => Physics2D.Raycast(transform.position, Vector2.right * isFacingDir, _attackCheckDis, isPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + _attackCheckDis * isFacingDir, transform.position.y));
        Gizmos.DrawWireSphere(_attackCheck.position, _attackRadius);
    }
}
