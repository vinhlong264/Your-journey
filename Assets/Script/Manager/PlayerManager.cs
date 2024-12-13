using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
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

    private void OnEnable()
    {
        Observer.onGainReward += receiveReward;
    }

    private void OnDisable()
    {
        Observer.onGainReward -= receiveReward;
    }


    private void Start()
    {
        levelSystem = new LevelSystem();
    }

    private void receiveReward(float reward)
    {
        currentExp += reward;
        if (levelSystem.gainExp(currentExp))
        {
            currentLevel = levelSystem.getCurrentLevel();
            currentExp = levelSystem.getExperience();
        }
    }
}
