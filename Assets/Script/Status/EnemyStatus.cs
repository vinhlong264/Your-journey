using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyStatus : CharacterStatus, ISubject
{
    private Enemy enemy;
    private itemDrop dropSystem;

    [SerializeField] int level;
    [Range(0, 1f)]
    [SerializeField] private float perCanStage;

    [SerializeField] private float expReward;

    protected override void Start()
    {
        applyPower();

        base.Start();
        enemy = GetComponent<Enemy>();
        dropSystem = GetComponent<itemDrop>();  

        
    }

    private void applyPower()
    {
        applyModifier(vitality);
        applyModifier(strength);
        applyModifier(dame);
    }

    private void applyModifier(Stat _status)
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
        eventCallBack(expReward);
        dropSystem.generateDrop();
        return;
    }

    public void eventCallBack(float value)
    {
        FindObserverManager().Listener(value);
    }

    private IObsever FindObserverManager()
    {
        return FindObjectsOfType<MonoBehaviour>().OfType<IObsever>().FirstOrDefault();
    }
}
