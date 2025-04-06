using System.Collections;
using UnityEngine;

public class Clone_Controller : SkillControllerBase
{
    private SpriteRenderer sr;
    private float percentDameExtra;

    private Transform closestEnemy; // vị trí của Enemy 
    [SerializeField] private float colorLosingSpeed; // Tốc độ giảm alpha

    [Header("Attack Infor")]
    [SerializeField] private Transform AttackCheck; // vị trí tấn công
    [SerializeField] private float AttackRadius; // phạm vi tấn công
    [SerializeField] private bool canAttack; // kiểm tra xem clone có được tấn công không

    [Header("Attack hit effect")]
    private bool canAttackWithEffect;

    [Header("Clone Mirage")]
    private bool canDuplicateClone; // kiểm tra xem có thể ra nhiều clone không
    private int isFacing = 1;

    protected override void Awake()
    {
        base.Awake();
        sr = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();
    }

    private void OnDisable()
    {
        sr.color = new Color(1, 1, 1, 1);
    }
    public void setUpClone(Transform _cloneTrasform, float _coolDown, bool _canAttack, Vector3 _offset
        , Transform _closestEnemy, bool _canAttackWithEffect, bool _canDuplicateClone, Player _player, float _percentDameExtra)
    {
        if (_canAttack)
        {
            anim.SetInteger("AttackNumber", Random.Range(1, 3));
        }


        transform.position = _cloneTrasform.position + _offset; // vị trí được khởi tạo
        coolDownTimer = _coolDown; // setup thời gian tồn tại của clone

        if (_closestEnemy != null)
        {
            closestEnemy = _closestEnemy;
        }
        canAttackWithEffect = _canAttackWithEffect;
        canDuplicateClone = _canDuplicateClone;
        player = _player;
        percentDameExtra = _percentDameExtra;

        FacingClone();
        StartCoroutine(CloneExitsDuration());
    }

    IEnumerator CloneExitsDuration()
    {
        float time = 0;
        while (time < coolDownTimer)
        {
            sr.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, time / coolDownTimer));
            time += Time.deltaTime * colorLosingSpeed;
            yield return null;
        }
        gameObject.SetActive(false);
    }

    //Hàm setup thuộc tính của Clone
    public void AnimationTrigger()
    {
        coolDownTimer = -.1f;
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

    protected override void SkillExcute()
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(AttackCheck.position, AttackRadius);

        foreach (Collider2D hit in col)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                AttackHandler(hit);
            }
        }
    }

    protected override void AttackHandler(Collider2D hitTarget)
    {
        IDameHandlePhysical damePhysical = hitTarget.GetComponent<IDameHandlePhysical>();

        if (damePhysical == null) return;

        damePhysical.DameHandlerPhysical(player.status);

        ItemEquipmentSO equipement = inventory.getEquipmentBy(EqipmentType.Sword);

        if (equipement != null)
        {
            if (canAttackWithEffect)
            {
                Debug.Log("Handler Effect");
                equipement.excuteItemEffect(hitTarget.transform);
            }
        }


        if (canDuplicateClone)
        {
            if (Random.Range(0, 100) >= 50)
            {
                skill.clone_skill.CreateClone(hitTarget.transform, new Vector3(0.5f * isFacing, 0, 0));
            }
        }
    }
}
