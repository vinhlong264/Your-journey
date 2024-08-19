using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clone_Skill : Skill
{
    [SerializeField] private GameObject Clone_Pre;
    public void CreateClone(Transform _clonePos)
    {
        GameObject newClone = Instantiate(Clone_Pre);
        newClone.GetComponent<Clone_Controller>().setUpClone(_clonePos , coolDown);
    }
   
}