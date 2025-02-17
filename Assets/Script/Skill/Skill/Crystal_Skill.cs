using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [Header("Simple Crystal skill")]
    [SerializeField] private UI_SkillTreeSlot crystalSkillUnlockBtn;
    public bool crystalSkillUnlocked;
    [SerializeField] private GameObject newCrystal; // Object prefabs
    [SerializeField] private GameObject currenrCrystal; // Object container

    [Header("Crystal with moving target")]
    [SerializeField] private UI_SkillTreeSlot crystalSkillMovingTargetBtn;
    [SerializeField] private bool crystalMovingTargetUnlock;  // có thể di chuyển tới Enemis
    [SerializeField] private float moveSpeed; // tốc độ di chuyển

    [Header("Crystal with explore")]
    [SerializeField] private UI_SkillTreeSlot crystalSkillExploreBtn;
    [SerializeField] private bool crystalExploreUnlock; // có thể phát nổ
    [SerializeField] private float crystalDuration; // coolDown

    [Header("Multi info")]
    [SerializeField] private UI_SkillTreeSlot crystalSkillMultiUnlockBtn;
    [SerializeField] private bool crystalMultiUnlock;
    [SerializeField] private float multiCooldown; // coolDown của Multi
    [SerializeField] private int amountOfMulti; // số lượng multi
    [SerializeField] private List<GameObject> listCrystal = new List<GameObject>();// List chứa crystal

    [Header("Crystal with Mirage")]
    [SerializeField] private UI_SkillTreeSlot crystalSkillWithMirageBtn;
    public bool crystalCreateMirage; // có thể sinh ra clone tại vị trí Pos

    public System.Action CompeleteSkill;
    public System.Action eventCallBack;

   

    protected override void Start()
    {
        base.Start();

        //Đăng kí các event
        crystalSkillUnlockBtn.eventUnlockSKill += onCrystalUnlock;
        crystalSkillExploreBtn.eventUnlockSKill += onCrystalWithExploreUnlock;
        crystalSkillMovingTargetBtn.eventUnlockSKill += onCrystalWithMovingTargetUnlock;
        crystalSkillMultiUnlockBtn.eventUnlockSKill += onCrystalWithMultiUnclock;


        crystalSkillWithMirageBtn.eventUnlockSKill += onCrystalWithMirageUnlock;

        eventCallBack += EventCallBackHandler;
        refillCrystal();
    }

    public override void UseSkill()
    {
        base.UseSkill();
        if (CanUseMutilCystal()) // sử dụng skill multil-Cystal
        {
            return;
        }



        if (currenrCrystal == null) // Khởi tạo Crystal
        {
            createCrystal();
        }
        else
        {
            if (crystalMovingTargetUnlock) return;

            Vector2 PlayerPos = player.transform.position; // Lấy ra vị trí của Player

            player.transform.position = currenrCrystal.transform.position; //Hóa đổi vị trí Player và Crystal 

            currenrCrystal.transform.position = PlayerPos; // đổi chỗ cho Player

            if (crystalCreateMirage) // cho phép sinh ra clone tại vị trí của Crystal ngay khi đổi chỗ
            {
                SkillManager.instance.clone_skill.CreateClone(currenrCrystal.transform, Vector3.zero);
                currenrCrystal = null;
            }
            else
            {
                currenrCrystal.GetComponent<Crystal_Skill_Controller>().FinishCrystal();
            }
            CompeleteSkill?.Invoke();
            coolDownTimer = coolDown;
            currenrCrystal = null;
        }

        //StartCoroutine(delayCoolDown());
    }

    private void EventCallBackHandler()
    {
        CompeleteSkill?.Invoke();
        currenrCrystal = null;
    }

    

    IEnumerator delayCoolDown()
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Start CoolDown");
        coolDownTimer = coolDown;
    }

    public void createCrystal()
    {
        currenrCrystal = GameManager.Instance.GetObjFromPool(newCrystal);
        currenrCrystal.transform.position = player.transform.position;
        currenrCrystal.transform.rotation = Quaternion.identity;
        Crystal_Skill_Controller curentCrystalScript = currenrCrystal.GetComponent<Crystal_Skill_Controller>();
        curentCrystalScript.setUpCrystal(crystalDuration, moveSpeed, crystalExploreUnlock, crystalMovingTargetUnlock, findToClosestEnemy(currenrCrystal.transform), player);
    }

    #region Event unlock skill
    private void onCrystalUnlock()
    {
        if (crystalSkillUnlockBtn.isUnlocked)
        {
            crystalSkillUnlocked = true;
        }
    }

    private void onCrystalWithExploreUnlock()
    {
        if (crystalSkillExploreBtn.isUnlocked)
        {
            crystalExploreUnlock = true;
        }
    }

    private void onCrystalWithMovingTargetUnlock()
    {
        if (crystalSkillMovingTargetBtn.isUnlocked)
        {
            crystalMovingTargetUnlock = true;
        }
    }

    private void onCrystalWithMirageUnlock()
    {
        if (crystalSkillWithMirageBtn.isUnlocked)
        {
            crystalCreateMirage = true;
        }
    }

    private void onCrystalWithMultiUnclock()
    {
        if (crystalSkillMultiUnlockBtn.isUnlocked)
        {
            crystalMultiUnlock = true;
        }
    }

    #endregion

    private bool CanUseMutilCystal()
    {
        int i = 0;

        if (crystalMultiUnlock)
        {
            if (listCrystal.Count > 0)
            {
                coolDown = 0;
                GameObject CrystalSpawn = listCrystal[listCrystal.Count - 1];
                GameObject _newCrystal = GameManager.Instance.GetObjFromPool(CrystalSpawn);
                _newCrystal.transform.position = player.transform.position;
                _newCrystal.transform.rotation = Quaternion.identity;
                listCrystal.Remove(CrystalSpawn); // xóa khỏi list khi nó đc lấy ra

                _newCrystal.GetComponent<Crystal_Skill_Controller>()
                    .setUpCrystal(crystalDuration, moveSpeed, crystalExploreUnlock, crystalMovingTargetUnlock, findToClosestEnemy(_newCrystal.transform), player);

                if (listCrystal.Count <= 0f) // khi list rỗng sẽ tự động thêm và phải chờ một thời gian ms đc sử dụng
                {
                    Debug.Log("Dừng sử dụng Skill");
                    coolDown = multiCooldown;
                    coolDownTimer = multiCooldown;
                    refillCrystal();
                    return false;
                }

                return true;
            }
        }

        return false;
    }

    private void refillCrystal()
    {
        for (int i = 0; i < amountOfMulti; i++)
        {
            listCrystal.Add(newCrystal);
        }
    }
}
