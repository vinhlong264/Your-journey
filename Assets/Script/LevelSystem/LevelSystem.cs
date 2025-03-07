using UnityEngine;


[System.Serializable]
public class LevelSystem
{
    public int level;
    public int pointAtributte;
    public int pointSkill;


    // Thông tin về Exp
    public int currentExp;
    public int expToNextLevel;

    public LevelSystem()
    {
        level = 1;
        pointAtributte = 0;
        pointSkill = 0;
        currentExp = 0;
        expToNextLevel = 100;
    }

    public bool gainExp(int receiveExp)
    {
        if(receiveExp < 0) return false;

        currentExp += receiveExp;
        if(currentExp >= expToNextLevel)
        {
            levelUp();
            return true;
        }

        return false;
    }

    private void levelUp()
    {
        level++;
        pointAtributte += 5;
        pointSkill++;

        currentExp -= expToNextLevel;
        expToNextLevel = (int)(Mathf.Pow(level, 2) * 100f);
    }
}
