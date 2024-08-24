using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{
    private float CrystalExitTime;

    [SerializeField] private bool canExplore;
    private bool canGrow;
    [SerializeField] private float growSpeed;
    
    
    private float moveSpeed;
    private bool canMoveEnemies;
    private Transform closestTarget;



    private Animator animator;
    private CircleCollider2D cd;
    private void Start()
    {
        animator = GetComponent<Animator>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void setUpCrystal(float _crystalDuration, float _moveSpeed, bool _canExplore, bool _canMoveEnemies , Transform _closestTarget)
    {
        CrystalExitTime = _crystalDuration;
        moveSpeed = _moveSpeed;
        canExplore = _canExplore;
        canMoveEnemies = _canMoveEnemies;
        closestTarget = _closestTarget;
    }

    // Update is called once per frame
    void Update()
    {
        CrystalExitTime -= Time.deltaTime;
        if (CrystalExitTime < 0)
        {
            FinishCrystal();
        }

        if (canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale , new Vector2(3,3) , growSpeed * Time.deltaTime);  
        }


        if (canMoveEnemies)
        {
            transform.position = Vector2.MoveTowards(transform.position, closestTarget.position, moveSpeed * Time.deltaTime);
            if (Vector2.Distance(transform.position, closestTarget.position) < 0.5f)
            {
                FinishCrystal();
                canMoveEnemies = false;
            }
        }
    }

    public void FinishCrystal()
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

    private void AnimationAttackExplore()
    {
        Collider2D[] attackCheck = Physics2D.OverlapCircleAll(transform.position, cd.radius);

        foreach(var hit in attackCheck)
        {
            Enemy enemy = hit.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.takeDame(1);
            }
        }
    }

    private void selfDestroy()
    {
        Destroy(gameObject);
    }



}
