using UnityEngine;

public class LevelSystem
{
    private int level;
    private float experience;
    private float experienceToNextLevel;

    public LevelSystem()
    {
        level = 0;
        experience = 0;
        experienceToNextLevel = 100;
    }

    public bool gainExp(float receiveExp)
    {
        if(receiveExp < 0) return false;

        experience += receiveExp;
        if(experience >= experienceToNextLevel)
        {
            Debug.Log("Enough Exp");
            levelUp();
            return true;
        }

        Debug.Log("Not enough exp");
        return false;
    }

    private void levelUp()
    {
        level++;
        experience -= experienceToNextLevel;
        experienceToNextLevel = Mathf.Pow(level, 2) * 100f;
    }

    public int getCurrentLevel() => level;
    public float getExperience() => experience;
}
