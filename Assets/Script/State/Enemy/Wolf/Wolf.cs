public class Wolf : Enemy
{
    #region State
    public WolfIdleState _idleState { get; private set; }
    public WolfRunState _runState { get; private set; }
    public WolfBattleState _battleState { get; private set; }
    public WolfAttackState _attackState { get; private set; }
    public WolfStunState _stunState { get; private set; }
    public WolfDeathState _deathState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        _idleState = new WolfIdleState(this, stateMachine, "IDLE", this);
        _runState = new WolfRunState(this, stateMachine, "RUN", this);
        _battleState = new WolfBattleState(this , stateMachine , "RUN" , this);
        _attackState = new WolfAttackState(this, stateMachine, "ATTACK", this);
        _stunState = new WolfStunState(this, stateMachine, "STUN", this);
        _deathState = new WolfDeathState(this , stateMachine , "DEATH" , this);
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

    protected override void FixedUpdate()
    {
        stateMachine.currentState.FixUpdate();
    }

    public override bool checkStunned()
    {
        if (base.checkStunned())
        {
            stateMachine.changeState(_stunState);
            return true;
        }
        return false;
    }

    public override void Die()
    {
        stateMachine.changeState(_deathState);
        AttackImage.SetActive(false);
    }


}
