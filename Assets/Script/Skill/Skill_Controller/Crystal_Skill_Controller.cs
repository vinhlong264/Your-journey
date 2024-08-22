using UnityEngine;

public class Crystal_Skill_Controller : MonoBehaviour
{
    private float CrystalExitTime;

    [SerializeField] private bool canExplore;
    private bool canMoveEnemies;
    private float moveSpeed;
    private bool canGrow;
    [SerializeField] private float growSpeed;

    private Animator animator;
    private CircleCollider2D cd;
    private void Start()
    {
        animator = GetComponent<Animator>();
        cd = GetComponent<CircleCollider2D>();
    }

    public void setUpCrystal(float _crystalDuration, float _moveSpeed, bool _canExplore, bool _canMoveEnemies)
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
        if (CrystalExitTime < 0)
        {
            FinishCrystal();
        }

        if (canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale , new Vector2(3,3) , growSpeed * Time.deltaTime);  
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
