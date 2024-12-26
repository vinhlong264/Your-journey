using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class Dogde_Skill : Skill
{
    [Header("Dogde skill")]
    [SerializeField] private UI_SkillTreeSlot dogdeSkillUnlockBtn; 
    public bool dogdeSkillUnlocked;

    [Header("Dogde skill with Mirage")]
    [SerializeField] private UI_SkillTreeSlot dogdeWithMirageUnlockBtn;
    public bool dogdeWithMirageUnlocked;


    protected override void Start()
    {
        base.Start();
        dogdeSkillUnlockBtn.eventUnlockSKill += onDogdeSkillUnlock;
        dogdeWithMirageUnlockBtn.eventUnlockSKill += onDogdeWithMirageUnlock;
    }
    
    private void onDogdeSkillUnlock() // Unlock skill Dogde
    {
        if (dogdeSkillUnlockBtn.isUnlocked)
        {
            player.status.evasion.addModifiers(10);
            Inventory.Instance.updateStatsUI(); // Cập nhập lại stats
            dogdeSkillUnlocked = true;
        }
    }

    private void onDogdeWithMirageUnlock() // Unlock skill with Mirage
    {
        if (dogdeWithMirageUnlockBtn.isUnlocked)
        {
            dogdeWithMirageUnlocked = true;
        }
    }

    public void createMirageOnDogde(Transform _pos)
    {
        if (dogdeWithMirageUnlocked)
        {
            skillManager.clone_skill.CreateClone(_pos, new Vector2(2 * player.isFacingDir, 0));
        }
    }


}
