using System.Collections;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private SlimeType type;
    [SerializeField] private GameObject slimeSmall;

    public float lastTime;
    public float attackCoolDown;
    public float attackDistance;
    [SerializeField] private GameObject waterBulletPrefabs;

    #region State
    public SlimeIdleState idleState { get; private set; }
    public SlimeRunState runState { get; private set; }
    public SlimeBattleState battleState { get; private set; }
    public SlimeAttackState attackState { get; private set; }
    public SlimeStunState stunState { get; private set; }
    public SlimeDeathState deathState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        idleState = new SlimeIdleState(this, StateMachine, "IDLE", this);
        runState = new SlimeRunState(this, StateMachine, "RUN", this);
        battleState = new SlimeBattleState(this, StateMachine, "RUN", this);
        attackState = new SlimeAttackState(this, StateMachine, "ATTACK", this);
        stunState = new SlimeStunState(this, StateMachine, "STUN", this);
        deathState = new SlimeDeathState(this, StateMachine, "DEATH", this);

    }

    protected override void Start()
    {
        base.Start();
        StateMachine.initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        StateMachine.currentState.FixUpdate();
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

    public override void Die()
    {
        base.Die();
        StateMachine.changeState(deathState);
        //if(type == SlimeType.BIG)
        //{
        //    test();
        //}
    }

    private void test()
    {
        for(int i = 0; i < 3; i++)
        {
            GameObject newSlime = Instantiate(slimeSmall, transform.position, Quaternion.identity);
            newSlime.GetComponent<Rigidbody2D>().AddForce(transform.forward , ForceMode2D.Impulse);
        }
    }

    public void activeSkill()
    {
        GameObject newBullet = Instantiate(waterBulletPrefabs, new Vector2(transform.position.x, transform.position.y + 2), Quaternion.identity);
        WaterBullet wt = newBullet.GetComponent<WaterBullet>();
        if (wt != null)
        {
            StartCoroutine(startSkill(wt));
        }
    }

    IEnumerator startSkill(WaterBullet w)
    {
        yield return new WaitForSeconds(1f);
        w.setUp(3, true, PlayerManager.Instance.player.transform, GetComponent<EnemyStats>());
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

public enum SlimeType
{
    BIG,
    SMALL
}
