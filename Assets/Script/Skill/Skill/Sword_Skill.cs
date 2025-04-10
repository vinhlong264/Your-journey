﻿using UnityEngine;

public class Sword_Skill : Skill
{
    [SerializeField] private SwordType swordType;
    [SerializeField] private float speedReturning;

    [Header("Throw sword skill")]
    [SerializeField] private UI_SkillTreeSlot throwSwordUnlockBtn;
    public bool swordSkillUnlocked { get; private set; }

    [Header("Bounce sword skill")]
    [SerializeField] private UI_SkillTreeSlot bounceSwordUnlockBtn;
    [SerializeField] private int amoutOfBounce;
    [SerializeField] private float bounceGravity;

    [Header("Pierce sword skill")]
    [SerializeField] private UI_SkillTreeSlot pierceSwordUnlockbtn;
    [SerializeField] private int amountPierce;
    [SerializeField] private float pierceGravity;

    [Header("Spin info")]
    [SerializeField] private UI_SkillTreeSlot spinSwordUnlockBtn;
    [SerializeField] private float hitCooldown;
    [SerializeField] private float maxTravelDistance = 7f;
    [SerializeField] private float spinDuration = 2f;
    [SerializeField] private float spinGravity = 1f;


    [Header("Sword FreeTime Target")]
    [SerializeField] private UI_SkillTreeSlot freeTimeUnlockBtn;
    [SerializeField] private float FreezeTimer;
    private bool freeTimeUnlocked;

    [Header("Sword take vulnerable bleeding")]
    [SerializeField] private UI_SkillTreeSlot bleedingUnlockBtn;
    private bool bleedingUnlocked;
    

    [Header("Skill Sword Infor")]
    [SerializeField] private GameObject swordPrefabs; // sword clone
    [SerializeField] private Vector2 laughDirection; // Hướng ném của cây kiếm
    [SerializeField] private float swordGravity; // trọng lực
    private Vector2 FinalDir; // Hướng ném của cây kiếm sau khi tính toán

    [Header("Animation Dots infor")]
    [SerializeField] private int amountDots; // số lượng dots
    [SerializeField] private float BetweenSpaceDots; // khoảng cách giữa các dots
    [SerializeField] private GameObject dotsPrefabs;
    [SerializeField] private Transform dotsParent; // vị trí chứa các dots khi được khởi tạo

    [SerializeField] private GameObject[] dots;

    protected override void OnEnable()
    {
        if (throwSwordUnlockBtn.isUnlocked)
        {
            swordSkillUnlocked = true;
        }

        if (bounceSwordUnlockBtn.isUnlocked)
        {
            swordType = SwordType.BOUNCE;
            return;
        }

        if (pierceSwordUnlockbtn.isUnlocked)
        {
            swordType = SwordType.PIERCE;
            return;
        }

        if (spinSwordUnlockBtn.isUnlocked)
        {
            swordType = SwordType.SPIN;
            return;
        }
    }

    protected override void Start()
    {
        base.Start();
        InitializeDots();
        swordType = SwordType.REGULAR;

        throwSwordUnlockBtn.eventUnlockSKill += onThrowSwordUnlock;

        //branch sword type
        bounceSwordUnlockBtn.eventUnlockSKill += onBounceSwordUnlock;
        pierceSwordUnlockbtn.eventUnlockSKill += onPierceSwordUnlock;
        spinSwordUnlockBtn.eventUnlockSKill += onSpinSwordUnlock;

        //Branch time stop skill
        freeTimeUnlockBtn.eventUnlockSKill += onFreeTimeUnlock;
        bleedingUnlockBtn.eventUnlockSKill += onBleedingUnlock;

    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKey(KeyCode.Mouse1))
        {
            FinalDir = new Vector2(getDirectionMouse().normalized.x * laughDirection.x, getDirectionMouse().normalized.y * laughDirection.y);

            for (int i = 0; i < dots.Length; i++)
            {
                dots[i].transform.position = DotsPostion(i * BetweenSpaceDots);
            }
        }
    }


    public void createSword() // khởi tạo sword
    {
        GameObject newSword = GameManager.Instance.GetObjFromPool(swordPrefabs);
        newSword.transform.position = player.transform.position;
        newSword.transform.rotation = Quaternion.identity;

        Sword_Skill_Controller sw = newSword.GetComponent<Sword_Skill_Controller>();
        if (sw != null)
        {
            if (swordType == SwordType.BOUNCE)
            {
                swordGravity = bounceGravity;
                sw.isBounce(true, amoutOfBounce);
                Debug.Log("Đang ở bounce");
            }
            else if (swordType == SwordType.PIERCE)
            {
                swordGravity = pierceGravity;
                sw.isPierce(amountPierce);
                Debug.Log("Đang ở PIERCE ");
            }
            else if (swordType == SwordType.SPIN)
            {
                swordGravity = spinGravity;
                sw.isSpin(true, maxTravelDistance, spinDuration, hitCooldown);
                Debug.Log("Đang ở SPIN");
            }


            sw.setupSword(FinalDir, swordGravity, speedReturning, FreezeTimer, freeTimeUnlocked , bleedingUnlocked, player);
        }

        player.AsignNewSword(newSword); // gán sword hiện tại được ném ra cho Player

        DotsActive(false);
    }

    #region Unlock Skill
    private void onThrowSwordUnlock()
    {
        if (throwSwordUnlockBtn.isUnlocked)
        {
            swordSkillUnlocked = true;
        }
    }

    private void onBounceSwordUnlock()
    {
        if (bounceSwordUnlockBtn.isUnlocked)
        {
            swordType = SwordType.BOUNCE;
        }
    }

    private void onPierceSwordUnlock()
    {
        if (pierceSwordUnlockbtn.isUnlocked)
        {
            swordType = SwordType.PIERCE;
        }
    }

    private void onSpinSwordUnlock()
    {
        if (spinSwordUnlockBtn.isUnlocked)
        {
            swordType = SwordType.SPIN;
        }
    }

    private void onFreeTimeUnlock()
    {
        if (freeTimeUnlockBtn.isUnlocked)
        {
            freeTimeUnlocked = true;
        }
    }

    private void onBleedingUnlock()
    {
        if (bleedingUnlockBtn.isUnlocked)
        {
            bleedingUnlocked = true;
        }
    }
    #endregion

    #region Anim Dot
    private Vector2 getDirectionMouse() // hàm tính hướng của người chơi với con chuột trên màn hình
    {
        Vector2 playerPos = player.transform.position; // lấy vị trí của Player
        Vector2 MousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // Lấy vị trí của con trỏ chuột
        Vector2 dir = MousePos - playerPos; // Lấy hướng từ Player đến con trỏ chuột

        return dir;
    }

    public void DotsActive(bool _isActive) // hàm tắt bật các dots
    {
        for (int i = 0; i < dots.Length; i++)
        {
            dots[i].gameObject.SetActive(_isActive);
        }
    }

    private void InitializeDots() // khởi tạo các Dots
    {
        dots = new GameObject[amountDots];
        for (int i = 0; i < amountDots; i++)
        {
            dots[i] = Instantiate(dotsPrefabs, player.transform.position, Quaternion.identity, dotsParent.transform);
            dots[i].gameObject.SetActive(false);
        }
    }

    private Vector2 DotsPostion(float t) // hàm tính toán vị trí của các dots, hay nói đây là chuyển động cong đều
    {
        Vector2 dir = new Vector2(getDirectionMouse().normalized.x * laughDirection.x, getDirectionMouse().normalized.y * laughDirection.y);
        Vector2 postion = (Vector2)player.transform.position + dir * t + 0.5f * (Physics2D.gravity * swordGravity) * Mathf.Pow(t, 2);
        return postion;
    }
    #endregion
}


public enum SwordType // Các kiểu sword
{
    REGULAR,
    BOUNCE,
    PIERCE,
    SPIN
}
