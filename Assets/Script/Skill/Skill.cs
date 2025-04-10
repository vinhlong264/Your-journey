﻿using System.Collections;
using UnityEngine;
public abstract class Skill : MonoBehaviour
{
    [SerializeField] protected float coolDownTimer;
    [SerializeField] protected float coolDown;
    protected Player player;
    protected SkillManager skillManager;
    [SerializeField] protected LayerMask mask;
    public bool isUsing { get; set; }
    public float CoolDown { get => coolDown; }

    protected virtual void OnEnable()
    {

    }


    protected virtual void Start()
    {
        player = GameManager.Instance.Player;
        skillManager = GetComponent<SkillManager>();
    }

    protected virtual void Update()
    {
        
    }

    public virtual bool CanUseSkill()
    {
        if (coolDownTimer <= 0f)
        {
            //Sử dụng skill
            isUsing = true;
            return true;
        }
        Debug.Log("Đang hồi chiêu");
        return false;
    }


    public virtual void UseSkill()
    {
        // sử dụng những skill được chỉ định
    }

    public virtual void endSkill()
    {
        Debug.Log("Call");
        coolDownTimer = coolDown;
        isUsing = false;
        StartCoroutine(CoolDownSkill());
    }

    protected IEnumerator CoolDownSkill()
    {
        while (coolDownTimer > 0f)
        {
            Debug.Log("Thực hiện coolDown Skill");
            coolDownTimer -= Time.deltaTime;
            yield return null;
        }
    }

    // Hàm dùng để tìm ra vị trí gần nhất của Enemy vs các Skill cần lấy vị trí
    protected virtual Transform findToClosestEnemy(Transform _checkTransform)
    {
        Collider2D[] col = Physics2D.OverlapCircleAll(_checkTransform.position, 25 , mask);
        float closesDistance = Mathf.Infinity; // đại diện 1 giá trị dương vô cùng

        Transform closestEnemy = null;

        foreach (Collider2D hit in col)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
                float distaceToEnenmy = Vector2.Distance(_checkTransform.position, hit.transform.position); // lấy ra khoảng cách của Player và Enemy
                //Debug.Log(distaceToEnenmy);
                if (distaceToEnenmy < closesDistance) // nếu khoảng cách lấy ra nhỏ hơn khoảng cách đã lưu
                {
                    closesDistance = distaceToEnenmy; // gán lại giá trị closesDistance
                    closestEnemy = hit.transform; // lấy ra vị trí của Enemy
                }
            }
        }

        return closestEnemy;
    }
}
