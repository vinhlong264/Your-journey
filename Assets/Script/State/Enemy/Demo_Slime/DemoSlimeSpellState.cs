using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoSlimeSpellState : EnemyState
{
    private DemoSlime demoSlime;
    private int amoutSpell;
    private int countSpell;
    private float spellTime;
    private float coolDown;

    public DemoSlimeSpellState(Enemy enemyBase, EnemyStateMachine stateMachine, string animationBoolName , DemoSlime demoSlime) : base(enemyBase, stateMachine, animationBoolName)
    {
        this.demoSlime = demoSlime;
    }

    public override void Enter()
    {
        base.Enter();
        demoSlime.rb.bodyType = RigidbodyType2D.Kinematic;
        demoSlime.setZeroVelocity();
        amoutSpell = 15;
        stateTimer = demoSlime.SpellTime;
        countSpell = 0;
        coolDown = 0.75f;
    }

    public override void Update()
    {
        base.Update();
        spellTime -= Time.deltaTime;
        if (spellTime < 0 && countSpell < amoutSpell)
        {
            spellTime = coolDown;
            countSpell++;
            demoSlime.initializeFireFall();
        }

        if(countSpell >= amoutSpell)
        {
            stateMachine.changeState(demoSlime._battleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        demoSlime.rb.bodyType = RigidbodyType2D.Dynamic;
        demoSlime.lastTime = Time.time;
    }
    
}
