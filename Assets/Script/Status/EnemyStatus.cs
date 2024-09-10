using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyStatus : CharacterStatus
{
    private Enemy enemy;

    [SerializeField] int level;
    [Range(0, 1f)]
    [SerializeField] private float perCanStage;
    protected override void Start()
    {
        applyPower();

        base.Start();
        enemy = GetComponent<Enemy>();
    }

    private void applyPower()
    {
        applyModifier(vitality);
        applyModifier(strength);
        applyModifier(dame);
    }

    private void applyModifier(Status _status)
    {
        for(int i = 1; i < level; i++)
        {
            float modifrer = _status.getValue() * perCanStage;
            _status.addModifiers(Mathf.RoundToInt(modifrer));
        }
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
