using System.Collections;
using UnityEngine;

public class Enemy : Entity
{
    [Header("Attack info")]
    public float battleTime;
    public Transform AttackCheck;
    public float attackCheckDis;
    public float attackRadius;
    public LayerMask isPlayer;


    [Header("Stun info")]
    public float stunDuration;
    public Vector2 stunDirection;
    [SerializeField] protected bool isCanStun;
    [SerializeField] GameObject CounterImage;


    public float DefaultSpeed;

    public EnemyStateMachine StateMachine {  get; private set; }
    public override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine();
    }

    public override void Start()
    {
        base.Start();
        DefaultSpeed = moveSpeed;
    }
    public override void Update()
    {
        base.Update();
        StateMachine.currentState.Update();
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

    protected virtual IEnumerator FreezeForTimer(float _second)
    {
        FreezeToTimer(true);
        Debug.Log("Đang bị đóng băng");
        yield return new WaitForSeconds(_second);
        FreezeToTimer(false);
        Debug.Log("Kết thúc đóng băng");
    }


    public virtual void Dame()
    {

    }

    public void animationTriggerFinish() => StateMachine.currentState.AnimationTriggerCalled();

    public RaycastHit2D isPlayerDetected() => Physics2D.Raycast(transform.position, Vector2.right * isFacingDir, attackCheckDis, isPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackCheckDis * isFacingDir, transform.position.y));
        Gizmos.DrawWireSphere(AttackCheck.position, attackRadius);
    }
}
