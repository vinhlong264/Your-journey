using System.Collections.Generic;
using UnityEngine;

public class Crystal_Skill : Skill
{
    [SerializeField] GameObject newCrystal;
    [SerializeField] GameObject currenrCrystal;

    [Header("Crystal info")]
    [SerializeField] private float crystalDuration;
    [SerializeField] private bool canExplore;
    [SerializeField] private bool canMoveEnemies;
    [SerializeField] private float moveSpeed;

    [Header("Multi info")]
    [SerializeField] private bool canMultiCrystal;
    [SerializeField] private float multiCooldown;
    [SerializeField] private int amountOfMulti;
    [SerializeField] private List<GameObject> listCrystal = new List<GameObject>();
    public override void UseSkill()
    {
        base.UseSkill();

        if (CanUseMutilCystal())
        {
            return;
        }



        if (currenrCrystal == null)
        {
            currenrCrystal = Instantiate(newCrystal , player.transform.position , Quaternion.identity);
            Crystal_Skill_Controller curentCrystalScript = currenrCrystal.GetComponent<Crystal_Skill_Controller>();
            curentCrystalScript.setUpCrystal(crystalDuration , moveSpeed , canExplore , canMoveEnemies , findToClosestEnemy(currenrCrystal.transform));
        }
        else
        {
            if (canMoveEnemies) return;

            Vector2 PlayerPos = player.transform.position;

            player.transform.position = currenrCrystal.transform.position;

            currenrCrystal.transform.position = PlayerPos;

            currenrCrystal.GetComponent<Crystal_Skill_Controller>().FinishCrystal();
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
                listCrystal.Remove(CrystalSpawn);
                newCrystal.GetComponent<Crystal_Skill_Controller>()
                    .setUpCrystal(crystalDuration, moveSpeed, canExplore, canMoveEnemies, findToClosestEnemy(newCrystal.transform));

                if(listCrystal.Count <= 0f)
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


    private void refillCrystal()
    {
        for(int i = 0; i < amountOfMulti; i++)
        {
            listCrystal.Add(newCrystal);
        }
    }
}
