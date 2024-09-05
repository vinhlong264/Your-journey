using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : CharacterStatus
{
    private Enemy enemy;
    protected override void Start()
    {
        base.Start();
        enemy = GetComponent<Enemy>();
    }

    public override void takeDame(int _dame)
    {
        base.takeDame(_dame);
        enemy.dameEffect();
    }

    protected override void Die()
    {
        base.Die();
        enemy.Die();
    }
}
