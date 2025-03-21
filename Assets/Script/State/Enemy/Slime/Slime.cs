using System.Collections;
using UnityEngine;

public class Slime : Enemy
{
    [SerializeField] private SlimeType type;
    [SerializeField] private GameObject slimeSmall;
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
        idleState = new SlimeIdleState(this, stateMachine, "IDLE", this);
        runState = new SlimeRunState(this, stateMachine, "RUN", this);
        battleState = new SlimeBattleState(this, stateMachine, "RUN", this);
        attackState = new SlimeAttackState(this, stateMachine, "ATTACK", this);
        stunState = new SlimeStunState(this, stateMachine, "STUN", this);
        deathState = new SlimeDeathState(this, stateMachine, "DEATH", this);

    }

    protected override void Start()
    {
        base.Start();
        stateMachine.initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
    }

    protected override void FixedUpdate()
    {
        stateMachine.currentState.FixUpdate();
    }

    public override bool checkStunned()
    {
        if (base.checkStunned())
        {
            stateMachine.changeState(stunState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        base.Die();
        CloseAttackWindow();
        stateMachine.changeState(deathState);
        //if (type == SlimeType.SMALL) return;

        //if (slimeSmall == null) return;

        //for(int i = 0; i < 3 ; i++)
        //{
        //    Vector3 temPos = new Vector3(Random.Range(10f,-10f), transform.position.y + Random.Range(12, 15), 0);
        //    GameObject newSlime = Instantiate(slimeSmall , transform.position , Quaternion.identity);
        //    newSlime.GetComponent<Rigidbody2D>().velocity = temPos;
        //}
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
        w.setUp(3, true, GameManager.Instance.Player.transform, GetComponent<EnemyStats>());
    }


    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
    }
}

public enum SlimeType
{
    BIG,
    SMALL,
    BOSS
}
