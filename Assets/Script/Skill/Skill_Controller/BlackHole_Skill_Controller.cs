﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole_Skill_Controller : SkillControllerBase
{
    [Header("HotKey info")]
    [SerializeField] private GameObject hotKeyPrefabs;
    [SerializeField] private List<KeyCode> ListHotKey; // List HotKey sẽ xuất hiện
    private bool canCreateHotKey = true; // biến kiểm tra việc tạo ra hotKey

    [Header("Blackhole info")]
    private float maxSize; // kích thước
    private float growSpeed; // tốc độ phát triển
    private float shrinkSpeed; // tốc độ thu lại

    private bool canGrow = true; // kiểm tra xem có thể phát triển không
    public bool canShrink; // Kiểm tra xem có thể thu lại không

    [Header("List object")]
    [SerializeField] private List<Transform> target = new List<Transform>(); // Dùng để lưu trữ Enemy khi ấn các nút
    private List<GameObject> createHotKey = new List<GameObject>(); //Lưu trữ các GameObject HotKey sau khi được khởi tạo 

    [Header("Clone info")]
    public int amountOfCloneAttack; // số lượng clone
    private bool cloneAttackReleased; // biến kiểm tra clone
    private float cloneAttackTimer; // cool down
    public float cloneAttackCooldown = 0.3f;


    private Vector3 defaultLocalScale;
    private bool playerCanDispear = true;

    public bool playerCanExitState { get; private set; }    // biến kết nối để kiểm soát việc thoát khỏi trạng thái này

    //Hàm setUp thuộc tính cho skill này
    public void setUpBlackHole(float _maxSize , float _growSpeed, float _shrinkSpeed, int _amountOfCloneAttack , float _cloneAttackCoolDown , float _blackHoleDuration)
    {
        maxSize = _maxSize;
        growSpeed = _growSpeed;
        shrinkSpeed = _shrinkSpeed;
        amountOfCloneAttack = _amountOfCloneAttack;
        cloneAttackCooldown = _cloneAttackCoolDown;
        coolDownTimer = _blackHoleDuration;
    }

    protected override void Start()
    {
        base.Start();
        defaultLocalScale = transform.localScale;
    }

    private void OnDisable()
    {
        canGrow = false;
        canShrink = true;
    }



    protected override void Update()
    {
        base.Update();
        cloneAttackTimer -= Time.deltaTime;

        if (coolDownTimer < 0)
        {
            coolDownTimer = Mathf.Infinity;
            if (target.Count > 0)
            {
                CloneAttackLogic();
            }
            else
            {
                BlackHoleFinish();
            }
        }


        SkillExcute();
    }

    private void growAndShrinkLogic() // Logic tăng và giảm phạm vi của black hole
    {
        if (canGrow && !canShrink) // kiểm tra xem có được phép phát triển phạm vi của black Hole không
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }

        if (canShrink) // Kiểm tra sự thu lại của Black Hole
        {
            transform.localScale = Vector2.Lerp(transform.localScale, new Vector3(-1, -1, 0), shrinkSpeed * Time.deltaTime);
            if (transform.localScale.x < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void ReleaseCloneAttack()
    {
        DestroyHotKey();
        cloneAttackReleased = true;
        canCreateHotKey = false;


        if (playerCanDispear)
        {
            playerCanDispear = false;
            GameManager.Instance.Player.makeTransprent(true);
        }
    }

    private void CloneAttackLogic()
    {
        if (target.Count <= 0) return;

        if (cloneAttackTimer < 0 && cloneAttackReleased && amountOfCloneAttack > 0)
        {
            cloneAttackTimer = cloneAttackCooldown;
            int indexTarget = Random.Range(0, target.Count);

            int xOffSet; // tạo ra khoảng cách khi tạo ra các clone ở bên trái phải phải enemy

            if (Random.Range(0, 10) > 5) // random khoảng cách với mỗi Enemy hay là index của target
            {
                xOffSet = 2;
            }
            else
            {
                xOffSet = -2;
            }
            //sinh ra mỗi clone tại các vị trí Index của target
            skill.clone_skill.CreateClone(target[indexTarget], new Vector3(xOffSet, 0, 0));
            amountOfCloneAttack--;
            if (amountOfCloneAttack <= 0)
            {
                StartCoroutine(BlackHoleFinishDelay());
            }
        }
    }

    IEnumerator BlackHoleFinishDelay()
    {
        yield return new WaitForSeconds(1f);
        BlackHoleFinish();
    }

    private void BlackHoleFinish()
    {
        DestroyHotKey();
        playerCanExitState = true; // Thoát khỏi trạng thái Black Hole state
        canShrink = true;
        cloneAttackReleased = false;
    }

    private void DestroyHotKey() // xóa các key
    {
        if (createHotKey.Count <= 0) return;

        foreach(GameObject i  in createHotKey)
        {
            i.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Enemy>() != null)
        {

            collision.GetComponent<Enemy>().FreezeToTimer(true); // đóng băng chuyển động của Enemy

            CreateHotKey(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {
            collision.GetComponent<Enemy>().FreezeToTimer(false); // Phá băng
        }
    }

    private void CreateHotKey(Collider2D collision) // Quản lý việc tạo ra các HotKey
    {
        if (ListHotKey.Count <= 0) // ngăn việc List rỗng nhưng hàm này vẫn được chạy
        {
            return;
        }

        if (!canCreateHotKey)
        {
            return;
        }

        GameObject newHotKey = GameManager.Instance.GetObjFromPool(hotKeyPrefabs);
        if (newHotKey == null) return;

        newHotKey.transform.position = collision.transform.position + new Vector3(0, 2, 0);
        newHotKey.transform.rotation = Quaternion.identity;

        createHotKey.Add(newHotKey);

        KeyCode chooseKey = ListHotKey[Random.Range(0, ListHotKey.Count)]; //Lấy ra ngẫu nhiên HotKey

        ListHotKey.Remove(chooseKey); // Xóa đi những KeyCode trong List khi chúng đc lấy ra

        BlackHole_HotKey_Controller hotKetScript = newHotKey.GetComponent<BlackHole_HotKey_Controller>();
        if (hotKetScript != null)
        {
            hotKetScript.setUpKeyCode(chooseKey, collision.transform, this);
        }
    }


    public void addEnemy(Transform _enemyTransform) => target.Add(_enemyTransform); // Lưu vị trí của các Enemy vào trong target

    protected override void SkillExcute()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReleaseCloneAttack();
        }

        CloneAttackLogic();

        growAndShrinkLogic();
    }

    protected override void AttackHandler(Collider2D hitTarget)
    {
        
    }
}
