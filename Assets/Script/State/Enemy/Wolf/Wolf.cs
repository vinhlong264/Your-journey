public class Wolf : Enemy
{
    public WolfIdleState _idleState { get; private set; }
    public WolfRunState _runState { get; private set; }
    public WolfBattleState _battleState { get; private set; }
    public WolfAttackState _attackState { get; private set; }
    protected override void Awake()
    {
        base.Awake();
        _idleState = new WolfIdleState(this, stateMachine, "IDLE", this);
        _runState = new WolfRunState(this, stateMachine, "RUN", this);
        _battleState = new WolfBattleState(this , stateMachine , "RUN" , this);
        _attackState = new WolfAttackState(this, stateMachine, "ATTACK", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.initialize(_idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        base.FixedUpdate();
    }


}
