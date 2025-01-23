using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static GameManager instance;

    public Player player;
    public PlayerStats playerStats;
    public PlayerLevel playerLevel;

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public bool levelupAtribute(StatType statType)
    {
        
        return false;
    }
}
