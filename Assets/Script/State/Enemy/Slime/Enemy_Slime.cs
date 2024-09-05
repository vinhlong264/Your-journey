using UnityEngine;

public enum SlimeType
{
    slimeSmall,
    slimeBig
}
public class Enemy_Slime : Enemy
{
    public float attackDis;
    [Header("Timer info")]
    public float idleTimer;
    public float Lastime;
    public float attackCooldown;

    [Header("Health info")]
    public float hpMax;
    public float hpCurrent;

    [SerializeField] GameObject Slime;
    [SerializeField] int amountSline;
    [SerializeField] SlimeType slimeType;

    #region state
    public SlimeIdleState idleState { get; private set; }
    public SlimeRunState runState { get; private set; }
    public SlimeBatteState battleState { get; private set; }
    public SlimeAttackState attackState { get; private set; }
    #endregion
    public override void Awake()
    {
        base.Awake();
        idleState = new SlimeIdleState(this, StateMachine, "Idle", this);
        runState = new SlimeRunState(this, StateMachine, "Run", this);
        battleState = new SlimeBatteState(this, StateMachine, "Run", this);
        attackState = new SlimeAttackState(this, StateMachine, "Attack", this);
    }

    public override void Start()
    {
        base.Start();
        StateMachine.initialize(idleState);
        isFacingRight = false;
        isFacingDir = -1f;
        hpCurrent = hpMax;
    }

    public override void dameEffect()
    {
        base.dameEffect();
    }


    //void Die()
    //{
    //    if (hpCurrent <= 0)
    //    {
    //        animator.SetTrigger("Death");
    //        GetComponent<Collider2D>().enabled = false;
    //        //GetComponent<Enemy_Slime>().enabled = false;
    //        rb.bodyType = RigidbodyType2D.Static;
    //        if(slimeType == SlimeType.slimeSmall)
    //        {
    //            return;
    //        }

    //        createSlime(amountSline , Slime);
    //    }
    //}


    void createSlime(int slimeAmount, GameObject slime)
    {
        int r = Random.Range(0, 7);
        for (int i = 0; i < slimeAmount; i++)
        {
            GameObject mySlime = Instantiate(slime, new Vector3(r, transform.position.y, 0), Quaternion.identity);
        }
    }

    public override void Update()
    {
        base.Update();
    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}
