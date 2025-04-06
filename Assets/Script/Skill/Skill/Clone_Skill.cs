using System.Collections;
using UnityEngine;

public class Clone_Skill : Skill
{
    private float percentDameExtra;

    [Header("Clone info")]
    [SerializeField] private GameObject Clone_Pre;
    [SerializeField] private float cloneDuration;

    [Header("Clone can attack")]
    [SerializeField] private UI_SkillTreeSlot cloneCanAttackUnlockBtn;
    [SerializeField] private float cloneAttackPercent;
    private bool cloneAttackUnlocked;

    [Header("Clone attack clone hit effect")]
    [SerializeField] private UI_SkillTreeSlot agrresiveCloneUnlockBtn;
    [SerializeField] private float agrresiveClonePercent;
    private bool agrresiveCloneUnlocked;

    [Header("Create crystal instead of mirage")]
    [SerializeField] private UI_SkillTreeSlot crystalInsteadMirageUnlockBtn;
    private bool crystalInsteadMirageUnlocked;

    [Header("Multiple clone")]
    [SerializeField] private UI_SkillTreeSlot multipleCloneUnlockBtn;
    [SerializeField] private float cloneMultiplePercent;
    private bool multipleCloneUnlocked;


    public void CreateClone(Transform _clonePos, Vector3 _ofSet) // Hàm khởi tạo clone
    {
        if (crystalInsteadMirageUnlocked)
        {
            skillManager.crystal_skill.createCrystal();
            return;
        }


        GameObject newClone = GameManager.Instance.GetObjFromPool(Clone_Pre);

        if (newClone == null)
        {
            Debug.Log("Không tạo ra được clone");
            return;
        }

        newClone.GetComponent<Clone_Controller>().setUpClone(_clonePos, cloneDuration, cloneAttackUnlocked , _ofSet, 
            findToClosestEnemy(newClone.transform), agrresiveCloneUnlocked, multipleCloneUnlocked, player, percentDameExtra);
    }

    protected override void Start()
    {
        base.Start();
        cloneCanAttackUnlockBtn.eventUnlockSKill += onCloneCanAttackUnlock;
        agrresiveCloneUnlockBtn.eventUnlockSKill += onAgrresiveCloneUnlock;

        crystalInsteadMirageUnlockBtn.eventUnlockSKill += onCrystalInsteadMirageUnlock;
        multipleCloneUnlockBtn.eventUnlockSKill += onMultipleCloneUnlock;

    }

    #region Unlock skill

    private void onCloneCanAttackUnlock()
    {
        if (cloneCanAttackUnlockBtn.isUnlocked)
        {
            cloneAttackUnlocked = true;
            percentDameExtra = cloneAttackPercent;
        }
    }

    private void onAgrresiveCloneUnlock()
    {
        if (agrresiveCloneUnlockBtn.isUnlocked)
        {
            agrresiveCloneUnlocked = true;
            percentDameExtra = agrresiveClonePercent;
        }
    }

    private void onCrystalInsteadMirageUnlock()
    {
        if (crystalInsteadMirageUnlockBtn.isUnlocked)
        {
            crystalInsteadMirageUnlocked = true;
        }
    }

    private void onMultipleCloneUnlock()
    {
        if (multipleCloneUnlockBtn.isUnlocked)
        {
            multipleCloneUnlocked = true;
            percentDameExtra = cloneAttackPercent;
        }
    }

    #endregion


    #region Method Skills of Clone
    public void CreatCloneWhenMirrage(Transform _enemes) // tạo ra clone khi Player Counter
    {
            StartCoroutine(DelayCounterAttackWithCloneCroutine(_enemes.transform, new Vector3(2f * player.isFacingDir, 0)));
    }

    IEnumerator DelayCounterAttackWithCloneCroutine(Transform _transform, Vector3 offSet) // Delay
    {
        yield return new WaitForSeconds(0.4f);
        CreateClone(_transform, offSet);
    }
    #endregion

}
