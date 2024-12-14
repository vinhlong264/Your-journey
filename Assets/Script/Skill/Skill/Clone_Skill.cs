using System.Collections;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clone info")]
    [SerializeField] private GameObject Clone_Pre;
    [SerializeField] private float cloneDuration;

    [Header("Skills of clone")]
    [SerializeField] private bool CreateCloneCouterAttack;
    [SerializeField] private bool canDuplicateClone;

    public void CreateClone(Transform _clonePos, Vector3 _ofSet) // Hàm khởi tạo clone
    {
        GameObject newClone = Instantiate(Clone_Pre);
        newClone.GetComponent<Clone_Controller>().setUpClone(_clonePos, coolDown, _ofSet, findToClosestEnemy(newClone.transform), canDuplicateClone, player);
    }

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
