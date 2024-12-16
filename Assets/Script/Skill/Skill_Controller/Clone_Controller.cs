using UnityEngine;

public class Clone_Controller : MonoBehaviour
{
    private Animator anim;
    private SpriteRenderer sr;
    private Player player;
    private float percentDameExtra;

    private Transform closestEnemy; // vị trí của Enemy 
    [SerializeField] private float CoolDown; // kiểm soát thời gian hồi chiêu
    [SerializeField] private float colorLosingSpeed; // Tốc độ giảm alpha

    [Header("Attack Infor")]
    [SerializeField] private Transform AttackCheck; // vị trí tấn công
    [SerializeField] private float AttackRadius; // phạm vi tấn công
    [SerializeField] private bool canAttack; // kiểm tra xem clone có được tấn công không

    [Header("Clone Mirage")]
    private bool canDuplicateClone; // kiểm tra xem có thể ra nhiều clone không
    private int isFacing = 1;

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

    //Hàm setup thuộc tính của Clone
    public void setUpClone(Transform _cloneTrasform , float _coolDown , Vector3 _offset 
        , Transform _closestEnemy , bool _canDuplicateClone,Player _player , float _percentDameExtra)
    {
        if (canAttack)
        {
            anim.SetInteger("AttackNumber", Random.Range(1, 3));
        }


        transform.position = _cloneTrasform.position + _offset; // vị trí được khởi tạo
        CoolDown = _coolDown;

        closestEnemy = _closestEnemy;

        canDuplicateClone = _canDuplicateClone;
        player = _player;
        percentDameExtra = _percentDameExtra;

        FacingClone();
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
            if (hit.GetComponent<Enemy>() != null)
            {
                player.status.DoDameWithSkill(hit.GetComponent<CharacterStats>() , percentDameExtra);
                if (canDuplicateClone)
                {
                    if(Random.Range(0,100) < 99)
                    {
                        SkillManager.instance.clone_skill.CreateClone(hit.transform, new Vector3(0.5f * isFacing, 0, 0));
                    }
                }
            }
        }
    }

    private void FacingClone() // Hàm thay đổi hướng nhìn của Clone
    {
        if (closestEnemy != null)
        {
            if (transform.position.x > closestEnemy.position.x) // nếu clone ở hiện tại ở bên phải Enemy thì xoay sang trái
            {
                isFacing = -1;
                transform.Rotate(0, 180, 0);
            }
        }
    }
}
