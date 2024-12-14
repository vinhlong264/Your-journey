using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>, IObsever
{
    private static PlayerManager instance;
    public Player player;

    [Header("Level")]
    [SerializeField] private LevelSystem levelSystem;
    [SerializeField] private int currentLevel;
    [SerializeField] private float currentExp;

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
        }
    }
}
