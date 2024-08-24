using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole_Skill : Skill
{
    [SerializeField] private int amountOfCloneAttack;
    [SerializeField] private float cloneAttackCooldown;
    [SerializeField] private float blackHoleDuration;
    [Space]
    [SerializeField] private GameObject blackHolePrefabs;
    [SerializeField] private float maxSize;
    [SerializeField] private float growSpeed;
    [SerializeField] private float shrinkSpeed;

    BlackHole_Skill_Controller currentBlackHole;

    public override bool CanUseSkill()
    {
        return base.CanUseSkill();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        GameObject newBlackHole = Instantiate(blackHolePrefabs , player.transform.position , Quaternion.identity);
        currentBlackHole = newBlackHole.GetComponent<BlackHole_Skill_Controller>();
        if(currentBlackHole != null )
        {
            currentBlackHole.setUpBlackHole(maxSize, growSpeed, shrinkSpeed, amountOfCloneAttack, cloneAttackCooldown,blackHoleDuration);
        }
    }
    public bool skillCompelete()
    {
        if(!currentBlackHole) return false;

        if (currentBlackHole.playerCanExitState)
        {
            currentBlackHole = null;
            return true;
        }

        return false;
    }
}