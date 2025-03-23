using UnityEngine;

public class Crystal_Skill_Controller : SkillControllerBase
{
    [SerializeField] private bool canExplore; // kiểm tra có thể nổ không
    private bool canGrow;
    [SerializeField] private float growSpeed; // Kiểm tra có thể phát triển không 
    
    
    private float moveSpeed; // tốc độ
    private bool canMoveEnemies; // có thể di chuyển tới Enemies
    private Transform closestTarget; // vị trí gần nhất của Enemy để tấn công


    [SerializeField] private LayerMask whatIsMask;
    private CircleCollider2D cd;
    private Vector3 defaultScacle; // scale gốc
    protected override void Start()
    {
        base.Start();
        cd = GetComponent<CircleCollider2D>();
        defaultScacle = transform.localScale;
    }


    public void setUpCrystal(float _crystalDuration, float _moveSpeed, bool _canExplore, bool _canMoveEnemies , Transform _closestTarget , Player _player)
    {
        coolDownTimer = _crystalDuration;
        moveSpeed = _moveSpeed;
        canExplore = _canExplore;
        canMoveEnemies = _canMoveEnemies;
        closestTarget = _closestTarget;
        player = _player;
    }

    private void OnDisable() // Reset lại các chỉ số khi Deactive
    {
        coolDownTimer = 0f;
        moveSpeed = 0;
        canExplore = false;
        canGrow = false;
        canMoveEnemies = false;
        transform.localScale = defaultScacle;
        closestTarget = null;
        player = null;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (coolDownTimer < 0)
        {
            FinishCrystal(); // Điều khiển trạng thái của Crystal
        }

        if (canGrow) // tăng độ lớn của Crystal
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
            anim.SetTrigger("Explore");
        }
        else
        {
            selfDestroy();
        }
    }

    private void selfDestroy()
    {
        gameObject.SetActive(false);
        skill.crystal_skill.eventCallBack?.Invoke();
    }

    protected override void SkillAttack()
    {
        Collider2D[] attackCheck = Physics2D.OverlapCircleAll(transform.position, cd.radius, whatIsMask);

        foreach (var hit in attackCheck)
        {
            IDameHandleMagical dameMagical = hit.GetComponent<IDameHandleMagical>();
            if (dameMagical != null)
            {
                dameMagical.DameDoMagical(GameManager.Instance.PlayerStats);
            }
        }
    }

    protected override void AttackHandler(Collider2D hitTarget)
    {
        IDameHandleMagical dameMagical = hitTarget.GetComponent<IDameHandleMagical>();
        if (dameMagical != null)
        {
            dameMagical.DameDoMagical(player.status);
        }
    }
}
