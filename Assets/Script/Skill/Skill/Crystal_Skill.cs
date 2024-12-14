using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [SerializeField] private GameObject newCrystal; // Object prefabs
    [SerializeField] private GameObject currenrCrystal; // Object container

    [Header("Crystal Mirage")]
    [SerializeField] private bool canCreateCloneToPos; // có thể sinh ra clone tại vị trí Pos


    [Header("Crystal info")]
    [SerializeField] private float crystalDuration; // coolDown
    [SerializeField] private bool canExplore; // có thể phát nổ
    [SerializeField] private bool canMoveEnemies;  // có thể di chuyển tới Enemis
    [SerializeField] private float moveSpeed; // tốc độ di chuyển

    [Header("Multi info")]
    [SerializeField] private bool canMultiCrystal;
    [SerializeField] private float multiCooldown; // coolDown của Multi
    [SerializeField] private int amountOfMulti; // số lượng multi
    [SerializeField] private List<GameObject> listCrystal = new List<GameObject>();// List chứa crystal
    protected override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMutilCystal()) // sử dụng skill multil-Cystal
        {
            return;
        }



        if (currenrCrystal == null) // Khởi tạo Crystal
        {
            currenrCrystal = Instantiate(newCrystal , player.transform.position , Quaternion.identity);
            Crystal_Skill_Controller curentCrystalScript = currenrCrystal.GetComponent<Crystal_Skill_Controller>();
            curentCrystalScript.setUpCrystal(crystalDuration , moveSpeed , canExplore , canMoveEnemies , findToClosestEnemy(currenrCrystal.transform) , player);
        }
        else
        {
            if (canMoveEnemies) return;

            Vector2 PlayerPos = player.transform.position; // Lấy ra vị trí của Player

            player.transform.position = currenrCrystal.transform.position; //Hóa đổi vị trí Player và Crystal 

            currenrCrystal.transform.position = PlayerPos; // đổi chỗ cho Player

            if (canCreateCloneToPos) // cho phép sinh ra clone tại vị trí của Crystal ngay khi đổi chỗ
            {
                SkillManager.instance.clone_skill.CreateClone(currenrCrystal.transform, Vector3.zero);
                Destroy(currenrCrystal);
            }
            else
            {
                currenrCrystal.GetComponent<Crystal_Skill_Controller>().FinishCrystal();
            }
        }
    }


    private bool CanUseMutilCystal()
    {
        if (canMultiCrystal)
        {
            if (listCrystal.Count > 0)
            {
                //coolDown = 0;


                GameObject CrystalSpawn = listCrystal[listCrystal.Count - 1]; 
                GameObject newCrystal = Instantiate(CrystalSpawn, player.transform.position, Quaternion.identity);

                listCrystal.Remove(CrystalSpawn); // xóa khỏi list khi nó đc lấy ra

                newCrystal.GetComponent<Crystal_Skill_Controller>()
                    .setUpCrystal(crystalDuration, moveSpeed, canExplore, canMoveEnemies, findToClosestEnemy(newCrystal.transform), player);

                if(listCrystal.Count <= 0f) // khi list rỗng sẽ tự động thêm và phải chờ một thời gian ms đc sử dụng
                {
                    Debug.Log("Dừng sử dụng Skill");
                    coolDown = multiCooldown;
                    refillCrystal();
                }

                return true;
            }
        }

        return false;
    }


    private void refillCrystal() // Thêm 3 Crystal vào listCrystal
    {
        for(int i = 0; i < amountOfMulti; i++)
        {
            listCrystal.Add(newCrystal);
        }
    }
}
