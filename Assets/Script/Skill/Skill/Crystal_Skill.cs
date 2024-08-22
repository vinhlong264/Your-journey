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
    public override void UseSkill()
    {
        base.UseSkill();

        if (currenrCrystal == null)
        {
            currenrCrystal = Instantiate(newCrystal , player.transform.position , Quaternion.identity);
            Crystal_Skill_Controller curentCrystalScript = currenrCrystal.GetComponent<Crystal_Skill_Controller>();
            curentCrystalScript.setUpCrystal(crystalDuration , moveSpeed , canExplore , canMoveEnemies);
        }
        else
        {
            Vector2 PlayerPos = player.transform.position;

            player.transform.position = currenrCrystal.transform.position;

            currenrCrystal.transform.position = PlayerPos;

            currenrCrystal.GetComponent<Crystal_Skill_Controller>().FinishCrystal();
        }
    }
}
