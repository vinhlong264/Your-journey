using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSlimeIdleState : DemoSlimeGroundState
{
    public DemoSlimeIdleState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName, DemoSlime demoSlime) : base(enemyBase, stateMachine, animationBoolName, demoSlime)
    {
    }
}
