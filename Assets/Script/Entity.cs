using System;
using System.Collections;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed;

    [Header("HitKnock info")]
    [SerializeField] protected Vector2 knockBackDir;
    [SerializeField] protected float knockBackDuration;
    [SerializeField] protected bool isKnock;


    [Header("Collision info")]
    [SerializeField] protected float groundCheckDis;
    [SerializeField] protected float wallCheckDis;
    [SerializeField] protected Transform ground;
    [SerializeField] protected Transform wall;
    [SerializeField] protected LayerMask whatIsLayerMask;

    [Header("Flip infor")]
    public bool isFacingRight;
    public float isFacingDir;

    public Action onFliped;

    public bool IsKnock { get => isKnock; }

    #region Component
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }

    private SpriteRenderer sr;
    public Collider2D cd { get; private set; }
    public CharacterStats status { get; private set; }
    public EntityFx fx { get; private set; }
    #endregion

    protected virtual void Awake()
    {

    }

    protected virtual void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        fx = GetComponent<EntityFx>();
        status = GetComponent<CharacterStats>();

    }

    protected virtual void Update()
    {

    }

    protected virtual void FixedUpdate()
    {
        
    }

    public bool groundCheck() => Physics2D.OverlapCircle(ground.position, groundCheckDis, whatIsLayerMask); // check mặt đất
    public bool wallCheck() => Physics2D.Raycast(wall.position, Vector2.right * isFacingDir, wallCheckDis, whatIsLayerMask);// check tường

    public void setZeroVelocity()
    {
        if (isKnock) return;

        rb.velocity = Vector2.zero;
    }

    public void setVelocity(float xVelocity, float yVelocity)
    {
        if (isKnock) return; // nếu isKnock == true thì thoát khỏi hàm này

        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    private void FlipController(float _x) // hàm xoay nhân vật
    {
        if (_x > 0 && !isFacingRight) // nếu !isFacingRight nghĩa là ở bên trái và khi _x > 0 thì xoay về bên phải và tương tự với else if
        {
            Flip();
        }
        else if (_x < 0 && isFacingRight)
        {
            Flip();
        }
    }

    public void Flip()
    {
        isFacingDir *= -1;
        isFacingRight = !isFacingRight;
        transform.Rotate(0, 180, 0);

        if (onFliped != null)
        {
            onFliped();
        }
    }

    public void makeTransprent(bool _transprent)
    {
        if (_transprent)
        {
            sr.color = Color.clear;
        }
        else
        {
            sr.color = new Color(1, 1, 1, 1);
        }
    }


    public virtual void Die()
    {

    }

    public virtual void dameEffect()
    {
        fx.StartCoroutine("FlashFx");
        StartCoroutine("isKnockBack");
    }

    protected IEnumerator isKnockBack() // Coroutine tạo hiệu ứng knockBack
    {
        isKnock = true;
        rb.velocity = new Vector2(knockBackDir.x * -isFacingDir, knockBackDir.y); // tạo hiệu ứng knockBack với việc tính toán lấy đảo dấu với giá trị isfacing
        yield return new WaitForSeconds(knockBackDuration); // thời gian tồn tại hiệu ứng của KnockBack
        isKnock = false;
    }

    public virtual void slowEntityBy(float _slowPercentage, float _slowDuration)
    {

    }

    protected virtual void returnDefaultValue()
    {
        animator.speed = 1;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ground.position, groundCheckDis);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wall.position, new Vector3(wall.position.x + wallCheckDis * isFacingDir, wall.position.y));
    }
}
