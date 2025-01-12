using UnityEngine;

public class PlayerManager : Singleton<PlayerManager>
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

    public int CurrentLevel { get => currentLevel; }
    public float CurrentExp { get => currentExp; }

    protected override void Awake()
    {
        MakeSingleton(true);

        //Observer.Instance.subscribeListener(GameEvent.RewardExp, Listener);
    }

    //private void OnDisable()
    //{
    //    Observer.Instance.unsubscribeListener(GameEvent.RewardExp, Listener);
    //}

    private void Start()
    {
        levelSystem = new LevelSystem();
        currentLevel = levelSystem.getCurrentLevel();
    }

    public void Listener(object value)
    {
        Debug.Log("Exp Receive: "+value);
        currentExp +=  (int)value;
        //Observer.Instance.NotifyEvent(GameEvent.UpdateCurrentExp , currentExp);
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
