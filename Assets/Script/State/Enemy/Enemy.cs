using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Entity , IDamage
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
    public EnemyStateMachine StateMachine {  get; private set; }
    public override void Awake()
    {
        base.Awake();
        StateMachine = new EnemyStateMachine();
    }

    public override void Start()
    {
        base.Start();
    }
    public override void Update()
    {
        base.Update();
        StateMachine.currentState.Update();
    }


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

    public void animationTriggerFinish() => StateMachine.currentState.AnimationTriggerCalled();

    public RaycastHit2D isPlayerDetected() => Physics2D.Raycast(transform.position, Vector2.right * isFacingDir, attackCheckDis, isPlayer);

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackCheckDis * isFacingDir, transform.position.y));
        Gizmos.DrawWireSphere(AttackCheck.position, attackRadius);
    }

    public virtual void takeDame(float dame)
    {
        
    }
}
