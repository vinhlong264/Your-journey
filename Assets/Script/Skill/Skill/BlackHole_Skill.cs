using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole_Skill : Skill
{
    [SerializeField] private int amountOfCloneAttack;
    [SerializeField] private float cloneAttackCooldown;
    [Space]
    [SerializeField] private GameObject blackHolePrefabs;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newBlackHole = Instantiate(blackHolePrefabs , player.transform.position , Quaternion.identity);
        BlackHole_Skill_Controller blackHoleScript = newBlackHole.GetComponent<BlackHole_Skill_Controller>();
        if(blackHoleScript != null )
        {
            blackHoleScript.setUpBlackHole(maxSize, growSpeed, shrinkSpeed, amountOfCloneAttack, cloneAttackCooldown);
        }
    }

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }
}
