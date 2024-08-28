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

    public override void takeDame(float _dame)
    {
        base.takeDame(_dame);
        enemy.DameEffect();
    }
}
