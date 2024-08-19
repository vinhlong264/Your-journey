using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Move info")]
    public float moveSpeed;

    [Header("HitKnock info")]
    [SerializeField]protected Vector2 knockBackDir;
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

    #region Component
    public Animator animator { get; private set; }
    public Rigidbody2D rb { get; private set; }
    #endregion

    public virtual void Awake()
    {
        
    }

    public virtual void Start()
    {
        animator = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    public virtual void Update()
    {
        
    }

    public bool groundCheck() => Physics2D.OverlapCircle(ground.position, groundCheckDis, whatIsLayerMask); // check mặt đất
    public bool wallCheck() => Physics2D.Raycast(wall.position, Vector2.right * isFacingDir, wallCheckDis, whatIsLayerMask);// check tường

    public void setZeroVelocity()
    {
        if (isKnock) return;

        rb.velocity = Vector2.zero;
    }

    public void setVelocity(float xVelocity , float yVelocity)
    {
        if (isKnock) return; // nếu isKnock == true thì thoát khỏi hàm này

        rb.velocity = new Vector2 (xVelocity, yVelocity);
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
    }

    protected IEnumerator isKnockBack() // Coroutine tạo hiệu ứng knockBack
    {
        isKnock = true;
        rb.velocity = new Vector2(knockBackDir.x * -isFacingDir, knockBackDir.y); // tạo hiệu ứng knockBack với việc tính toán lấy đảo dấu với giá trị isfacing
        yield return new WaitForSeconds(knockBackDuration); // thời gian tồn tại hiệu ứng của KnockBack
        isKnock = false;
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(ground.position, groundCheckDis);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(wall.position, new Vector3(wall.position.x + wallCheckDis * isFacingDir, wall.position.y));
    }
}
