using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [Header("Clon info")]
    [SerializeField] private GameObject Clone_Pre;
    [SerializeField] private float cloneDuration;

    [SerializeField] private bool createDashCloneOnStart;
    [SerializeField] private bool createDashCloneOnOver;
    [SerializeField] private bool CreateCloneCouterAttack;

    public void CreateClone(Transform _clonePos , Vector3 _ofSet)
    {
        GameObject newClone = Instantiate(Clone_Pre);
        newClone.GetComponent<Clone_Controller>().setUpClone(_clonePos , coolDown , _ofSet , findToClosestEnemy(newClone.transform));
    }

    public void CreateDashOnStart()
    {
        if(createDashCloneOnStart)
        {
            CreateClone(player.transform, Vector3.zero);
        }
    }


    public void CreateDashCloneOnOver()
    {
        if (createDashCloneOnOver)
        {
            CreateClone(player.transform, Vector3.zero);
        }
    }

    public void CreateCloneCounterAttack(Transform _enemes)
    {
        if (CreateCloneCouterAttack)
        {
            StartCoroutine(DelayCounterCloneAttack(_enemes.transform, new Vector3(2f * player.isFacingDir, 0)));
        }
    }

    IEnumerator DelayCounterCloneAttack(Transform _transform , Vector3 offSet)
    {
        yield return new WaitForSeconds(0.4f);
        CreateClone(_transform , offSet);
    }
   
}
