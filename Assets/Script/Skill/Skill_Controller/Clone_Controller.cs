using UnityEngine;

public class Clone_Controller : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;
    private Transform closestEnemy;
    [SerializeField] private float CoolDown;
    [SerializeField] private float colorLosingSpeed;

    [Header("Attack Infor")]
    [SerializeField] private Transform AttackCheck;
    [SerializeField] private float AttackRadius;
    [SerializeField] private bool canAttack;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        CoolDown -= Time.deltaTime;
        if (CoolDown < 0)
        {
            sr.color = new Color(1f, 1f, 1f, sr.color.a - (colorLosingSpeed * Time.deltaTime));
            //New Color là 1 constructor tạo ra từ thư viện của Unity
            //Các tham số đầu vào: r là chỉ số màu đỏ từ 0-1
            //                     g là chỉ số màu xanh lá từ 0-1
            //                     b là chỉ số màu xanh nước biển 0 - 1
            //                     a là độ trong suốt 0 - 1

            if (sr.color.a < 0f)
            {
                Destroy(gameObject);
            }
        }
        
    }

    public void setUpClone(Transform _cloneTrasform , float coolDownTimer , Vector3 _offset)
    {
        if (canAttack)
        {
            anim.SetInteger("AttackNumber", Random.Range(1, 3));
        }


        transform.position = _cloneTrasform.position + _offset;
        CoolDown = coolDownTimer;

        FacingClone();
    }

    private void FacingClone()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(transform.position, 25);
        float closesDistance = Mathf.Infinity; // đại diện 1 giá trị dương vô cùng
        foreach(Collider2D hit in col)
        {
            if(hit.GetComponent<Enemy>() != null)
            {
                float distaceToEnenmy = Vector2.Distance(transform.position, hit.transform.position); // lấy ra khoảng cách của Player và Enemy
                //Debug.Log(distaceToEnenmy);
                if(distaceToEnenmy < closesDistance) // nếu khoảng cách lấy ra nhỏ hơn khoảng cách đã lưu
                {
                    closesDistance = distaceToEnenmy; // gán lại giá trị closesDistance
                    closestEnemy = hit.transform; // lấy ra vị trí của Enemy
                    //Debug.Log("closestEnemy: " + closestEnemy);

                }
            }
        }

        if(closestEnemy != null)
        {
            if(transform.position.x > closestEnemy.position.x) // nếu clone ở hiện tại ở bên phải Enemy thì xoay sang trái
            {
                transform.Rotate(0, 180, 0);
            }
        }
    }

    public void AnimationTrigger()
    {
        CoolDown = -.1f;
    }


    public void attackTrigger()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(AttackCheck.position, AttackRadius);
        
        foreach (Collider2D hit in col)
        {
            if (hit.GetComponent<IDamage>() != null)
            {
                hit.GetComponent<IDamage>().takeDame(5);
                Debug.Log(hit.gameObject);
                
            }
        }
    }
}
