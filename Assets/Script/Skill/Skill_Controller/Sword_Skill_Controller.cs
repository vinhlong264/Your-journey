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
    private float speedReturning;
    private float FrezeeTimer;

    [Header("Bounce info")]
    private float speedBouce = 15f;
    private bool isBouncing; // biến kiểm tra nảy
    private List<Transform> EnemyTarget; // biến lưu giá trị ví trí của các Enemy vào list
    private int amountBouncing; // Số lượng lần nảy
    private int indexTarget; // Biến để chuyển giao index giữa các vị trí ở list

    [Header("Pierce info")]
    private int amountPierce;

    [Header("Spin info")]
    private float maxTravelDistace;
    private float spinTimer;
    private float spinDuration;
    private bool wasStop;
    private bool isSpining;

    private float hitTimer;
    private float hitCoolDown;


    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        cd = GetComponent<Collider2D>();
        anim = GetComponentInChildren<Animator>();
    }

    public void setupSword(Vector2 _dir, float _gravityScale , float _speedReturning , float _FrezeeTimer , Player _player) // hàm quản lý chuyển động của sword
    {
        player = _player;
        rb.velocity = _dir;
        rb.gravityScale = _gravityScale;
        speedReturning = _speedReturning;
        FrezeeTimer = _FrezeeTimer;

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

    public void isSpin(bool _isSpining , float _maxTravelDistace , float _spinDuration , float _hitCoolDown)
    {
        isSpining = _isSpining;
        maxTravelDistace = _maxTravelDistace;
        spinDuration = _spinDuration;
        hitCoolDown = _hitCoolDown;
    }

    private void Update()
    {
        if(canRotation)
         transform.right = rb.velocity;

        

        SwordReturn();

        BounceLogic();

        SpinLogic();
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
            if (Vector2.Distance(transform.position, EnemyTarget[indexTarget].position) < .1f) // so sánh khoảng cách giữa sword và enemy
            {
                EnemyTarget[indexTarget].GetComponent<Enemy>().takeDame(1);
                indexTarget++;
                amountBouncing--;

                if (amountBouncing <= 0) // sau khi nảy hết số lượt quy định thì sẽ isBouncing = false, isReturning = true
                {
                    isBouncing = false; // dừng kĩ năng nảy của sword
                    isReturning = true; // Sword quay lại với Plater
                }

                if (indexTarget >= EnemyTarget.Count)// Reset lại Indextarget
                {
                    indexTarget = 0;
                }
            }
        }
    }
    #region Sword Spin Skill
    void SpinLogic()
    {
        if (isSpining)
        {
            stopWhenSpining();

            if(wasStop)
            {
                Vector2 posTarget = new Vector2(transform.position.x + 1, transform.position.y);
                transform.position = Vector2.MoveTowards(transform.position, posTarget, 1.5f * Time.deltaTime);


                spinTimer -= Time.deltaTime;
                if(spinTimer < 0)
                {
                    isSpining = false;
                    isReturning = true;
                }
            }

            hitTimer -= Time.deltaTime;
            if(hitTimer < 0) // thời gian nhận dame
            {
                hitTimer = hitCoolDown;
                Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);
                //Kiểm tra trong phạm vị của vòng tròn là có bao nhiêu Enenmy thì sẽ gán chúng vào list
                foreach (var hit in colliders)
                {
                    if (hit.GetComponent<Enemy>() != null)
                    {
                        hit.GetComponent<Enemy>().takeDame(1);
                    }
                }
            }
        }
    }

    void stopWhenSpining() // Kiểm soát việc dừng lại khi đi xa Player
    {
        Debug.Log(Vector2.Distance(player.transform.position, transform.position));
        if (Vector2.Distance(player.transform.position, transform.position) > maxTravelDistace && !wasStop)
        {
            wasStop = true;
            rb.constraints = RigidbodyConstraints2D.FreezePosition;
            spinTimer = spinDuration;
        }
    }
    #endregion


    public void ReturnSword()
    {
        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        //rb.isKinematic = false;
        transform.parent = null;
        isReturning = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isReturning) return; // khi isReturning == true sẽ thoát luôn khỏi hàm này để đảm bảo k thực hiện logic bên dưới

        if(collision.GetComponent<Enemy>() != null)
        {
            Enemy enemy = collision.GetComponent<Enemy>();
            SworDameController(enemy);
        }        

        if (collision.GetComponent<Enemy>() != null )
        {
            if(isBouncing && EnemyTarget.Count <= 0) // hàm kiểm tra để đủ điều kiện kích hoạt việc nảy qua lại sword
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

    private void SworDameController(Enemy enemy) // Method attack
    {
        enemy.takeDame(1);
        enemy.StartCoroutine("FreezeForTimer", FrezeeTimer);
    }

    void StuckInto(Collider2D collision)
    {
        if(amountPierce > 0 && collision.GetComponent<Enemy>() != null) // Logic để tạo skill Pierce của sword
        {
            amountPierce--;
            Debug.Log(amountPierce);
            return;
        }

        if (isSpining) // nhằm việc tránh khi đang ở Spin và gặp 1 Colision nào đó sẽ khiến nó dùng hoạt động và bị gán child
        {
            stopWhenSpining();
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
