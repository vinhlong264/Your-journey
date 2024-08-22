using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{
    private float CrystalExitTime;

    [SerializeField] private bool canExplore;
    private bool canMoveEnemies;
    private float moveSpeed;
    
    private Animator animator;


    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void setUpCrystal(float _crystalDuration , float _moveSpeed, bool _canExplore , bool _canMoveEnemies)
    {
        CrystalExitTime = _crystalDuration;
        moveSpeed = _moveSpeed;
        canExplore = _canExplore;
        canMoveEnemies = _canMoveEnemies;
    }

    // Update is called once per frame
    void Update()
    {
        CrystalExitTime -= Time.deltaTime;
        if(CrystalExitTime < 0)
        {
            if (canExplore)
            {
                animator.SetTrigger("Explore");
            }
        }
    }

    void selfDestroy()
    {
        Destroy(gameObject);
    }
}
