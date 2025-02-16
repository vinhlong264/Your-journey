﻿using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{
    private float CrystalExitTime; // Kiểm soát thời gian tồn tại của Crystal

    [SerializeField] private bool canExplore; // kiểm tra có thể nổ không
    private bool canGrow;
    [SerializeField] private float growSpeed; // Kiểm tra có thể phát triển không 
    
    
    private float moveSpeed; // tốc độ
    private bool canMoveEnemies; // có thể di chuyển tới Enemies
    private Transform closestTarget; // vị trí gần nhất của Enemy để tấn công


    [SerializeField] private LayerMask whatIsMask;
    private Animator animator;
    private CircleCollider2D cd;
    private Player player;
    private void Start()
    {
        animator = GetComponent<Animator>();
        cd = GetComponent<CircleCollider2D>();
    }


    public void setUpCrystal(float _crystalDuration, float _moveSpeed, bool _canExplore, bool _canMoveEnemies , Transform _closestTarget , Player _player)
    {
        CrystalExitTime = _crystalDuration;
        moveSpeed = _moveSpeed;
        canExplore = _canExplore;
        canMoveEnemies = _canMoveEnemies;
        closestTarget = _closestTarget;
        player = _player;
    }

    private void OnDisable()
    {
        CrystalExitTime = 0;
        moveSpeed = 0;
        canExplore = false;
        canMoveEnemies = false;
        closestTarget = null;
        player = null;
    }

    // Update is called once per frame
    void Update()
    {
        CrystalExitTime -= Time.deltaTime;
        if (CrystalExitTime < 0)
        {
            FinishCrystal(); // Điều khiển trạng thái của Crystal
        }

        if (canGrow) // Phát triển độ lớn của Crystal
        {
            transform.localScale = Vector2.Lerp(transform.localScale , new Vector2(3,3) , growSpeed * Time.deltaTime);  
        }


        if (canMoveEnemies && closestTarget != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestTarget.position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, closestTarget.position) < 0.5f) // đến khoảng cách chỉ định sẽ phát nổ và reset canMoveEnemies
            {
                FinishCrystal();
                canMoveEnemies = false;
            }
        }
    }

    public void FinishCrystal() // Điều khiển trạng thái của Crystal
    {
        if (canExplore)
        {
            canGrow = true;
            animator.SetTrigger("Explore");
        }
        else
        {
            selfDestroy();
        }
    }

    private void AnimationAttackExplore() // Take dame
    {
        Collider2D[] attackCheck = Physics2D.OverlapCircleAll(transform.position, cd.radius , whatIsMask);

        foreach(var hit in attackCheck)
        {
            IDameHandleMagical dameMagical = hit.GetComponent<IDameHandleMagical>();
            if (dameMagical != null)
            {
                dameMagical.DameDoMagical(GameManager.Instance.playerStats);
            }
        }
    }

    private void selfDestroy()
    {
        gameObject.SetActive(false);
    }



}
