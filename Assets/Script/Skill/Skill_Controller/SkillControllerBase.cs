using UnityEngine;

public abstract class SkillControllerBase : MonoBehaviour
{
    protected SkillManager skill;
    protected Inventory inventory;
    protected Player player;
    protected Animator anim;

    [Header("Skill Infor")]
    [SerializeField] protected float coolDownTimer; // thời gian hồi lại của các skill
    [SerializeField] protected LayerMask WhatIsMask;

    protected virtual void Awake()
    {
        anim = GetComponent<Animator>(); 
    }

    protected virtual void Start()
    {
        skill = GameManager.Instance.Skill;
        inventory = GameManager.Instance.Inventory;
    }

    protected virtual void Update()
    {
        coolDownTimer -= Time.deltaTime;
    }

    protected abstract void SkillExcute();
    protected abstract void AttackHandler(Collider2D hitTarget);
}
