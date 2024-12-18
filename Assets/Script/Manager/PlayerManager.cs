using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>, IObsever
{
    private static PlayerManager instance;
    public Player player;
    public PlayerStats playerStats;


    [Header("Level")]
    [SerializeField] private LevelSystem levelSystem;
    [SerializeField] private int currentLevel;
    [SerializeField] private float currentExp;
    [SerializeField] private int pointSkill;
    [SerializeField] private int pointAtribute;

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        levelSystem = new LevelSystem();
    }

    //private void receiveReward(float reward)
    //{
    //    currentExp += reward;
    //    if (levelSystem.gainExp(currentExp))
    //    {
    //        currentLevel = levelSystem.getCurrentLevel();
    //        currentExp = levelSystem.getExperience();
    //    }
    //}

    public void Listener(float value)
    {
        currentExp += value;
        if (levelSystem.gainExp(currentExp))
        {
            currentLevel = levelSystem.getCurrentLevel();
            currentExp = levelSystem.getExperience();
            pointSkill++;
            pointAtribute++;
        }
    }

    public bool levelupAtribute(StatType statType)
    {
        if(pointAtribute > 0)
        {
            PlayerStats playerStats = player.GetComponent<PlayerStats>();
            if(playerStats != null)
            {
                Debug.Log(playerStats);
                playerStats.increaseStats(statType);
                pointAtribute--;
            }
            return true;
        }

        return false;
    }
}
