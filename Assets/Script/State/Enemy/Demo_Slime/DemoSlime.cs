using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSlime : Enemy
{
    [SerializeField] private Transform attackCheck;

    #region State
    public DemoSlimeIdleState _idleState { get; private set; }
    public DemoSlimeRunState _runState { get; private set; }
    #endregion
    protected override void Awake()
    {
        base.Awake();
        _idleState = new DemoSlimeIdleState(this, stateMachine, "IDLE" , this);
        _runState = new DemoSlimeRunState(this , stateMachine, "RUN" , this);
    }

    protected override void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawLine(attackCheck.position , new Vector3(attackCheck.position.x + _attackDis * isFacingDir , attackCheck.position.y , attackCheck.position.z));
        Gizmos.DrawWireSphere(_attackArea.position , _attackRadius);
    }
}
