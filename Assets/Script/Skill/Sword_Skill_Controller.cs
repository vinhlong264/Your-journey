using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Sword_Skill_Controller : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    private Animator anim;
    private Collider2D cd;
    [SerializeField] private bool canRotation = true;

    private Player player;
    [SerializeField] private bool isReturning;
    private float speedReturning = 12;

    [Header("Bounce info")]
    [SerializeField] private float speedBouce;
    private bool isBouncing; // biến kiểm tra nảy
    [SerializeField] private List<Transform> EnemyTarget; // biến lưu giá trị ví trí của các Enemy vào list
    private int amountBouncing; // Số lượng lần nảy
    private int indexTarget; // Biến để chuyển giao index giữa các vị trí ở list

    [Header("Pierce info")]
    private int amountPierce;




    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    public void setupSword(Vector2 _dir, float _gravityScale , Player _player) // hàm quản lý chuyển động của sword
    {
        player = _player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;

        if(amountPierce <= 0)
        {
            anim.SetBool("Rotation", true);
        }
    }

    public void isBounce(bool _isBouncing , int _amountOfBouce)
    {
        isBouncing = _isBouncing;
        amountBouncing = _amountOfBouce;

        EnemyTarget = new List<Transform>();
    }


    public void isPierce(int _amountPierce)
    {
        amountPierce = _amountPierce;
    }

    private void FixedUpdate()
    {
        if(canRotation)
         transform.right = rb.velocity;


        SwordReturn();

        BounceLogic();
    }


    void SwordReturn() // Login kiểm soát sự quay lại cùa sword
    {
        if (isReturning)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speedReturning * Time.deltaTime);
            if (Vector2.Distance(transform.position, player.transform.position) < 2)
            {
                player.CatchTheSword();
            }
        }
    }

    void BounceLogic() // Login kiểm soát độ nảy của Sword
    {
        if (isBouncing && EnemyTarget.Count > 0)
        {
            transform.position = Vector2.MoveTowards(transform.position, EnemyTarget[indexTarget].position, speedBouce * Time.deltaTime);

            if (Vector2.Distance(transform.position, EnemyTarget[indexTarget].position) < .1f)
            {
                indexTarget++;
                amountBouncing--;
                if (amountBouncing <= 0) // sau khi nảy hết số lượt quy định thì sẽ isBouncing = false, isReturning = true
                {
                    isBouncing = false; // dừng kĩ năng nảy của sword
                    isReturning = true; // Sword quay lại với Plater
                }

                if (indexTarget >= EnemyTarget.Count)
                {
                    indexTarget = 0;
                }
            }
        }
    }

    public void ReturSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (isReturning) return;

        collision.GetComponent<Enemy>()?.takeDame(1);

        if (collision.GetComponent<Enemy>() != null )
        {
            if(isBouncing && EnemyTarget.Count <= 0) // hàm kiểm tra để đủ điều kiện kích hoạt việc tấn công qua lại sword
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 10);
                //Kiểm tra trong phạm vị của vòng tròn là có bao nhiêu Enenmy thì sẽ gán chúng vào list
                foreach(var hit in colliders)
                {
                    if(hit.GetComponent<Enemy>() != null)
                    {
                        EnemyTarget.Add(hit.transform); 
                    }
                }

            }
        }

        StuckInto(collision);
    }

    void StuckInto(Collider2D collision)
    {
        if(amountPierce > 0 && collision.GetComponent<Enemy>() != null)
        {
            amountPierce--;
            return;
        }

        canRotation = false;
        cd.enabled = false;
        rb.isKinematic = true;
        rb.constraints = RigidbodyConstraints2D.FreezeAll;

        if (isBouncing && EnemyTarget.Count > 0) return;
        //Điều kiện này nhằm để giúp sword có thể nảy qua lại giữa các Enemy mà k bị gán  transform.parent cho Enemy nào đó 

        anim.SetBool("Rotation", false);
        transform.parent = collision.transform;
    }
}
