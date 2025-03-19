using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgrateStats : MonoBehaviour, ISave
{
    [SerializeField] private StatType stasType;
    [SerializeField] private Button upgrateBtn;
    [SerializeField] private TextMeshProUGUI statsUpgrateText;
    [SerializeField] private int statsCount;
    [SerializeField] private UI_StatSlot UI_StatSlot;
    [SerializeField] private StatsData statsData;

    private PlayerLevel playerLevel;

    private void OnEnable()
    {
        
    }

    void Start()
    {
        UI_StatSlot = GetComponentInParent<UI_StatSlot>();
        playerLevel = GameManager.Instance.playerLevel;

        statsUpgrateText.text = "+" + statsCount;
        upgrateBtn.onClick.AddListener(() => upgrateStatsEvent());


        if (statsData.stats.TryGetValue(stasType, out int value))
        {
            Debug.Log($"Có dữ liệu : {stasType} - {value}");
            statsCount = value;
            statsUpgrateText.text = "+" + statsCount;
            return;
        }
        else
        {
            Debug.Log("Nạp dữ liệu");
            statsData.stats.Add(stasType, statsCount);
        }
    }
    public void LoadGame(GameData data)
    {
        if (data == null)
        {
            Debug.Log("Không tồn tại data Stats");
            data.stats = new SerializableDictionary<StatType, int>();
            return;
        }

        statsData.stats = data.stats;

        if (statsData.stats.TryGetValue(stasType, out int value))
        {
            Debug.Log($"{stasType} {value}");
            statsCount = value;
        }
    }

    public void SaveGame(ref GameData data)
    {
        if (statsData.stats.TryGetValue(stasType, out int value))
        {
            Debug.Log(stasType.ToString() + ": " + value);
            if (data.stats.ContainsKey(stasType))
            {
                data.stats[stasType] = value;
            }
        }
    }



    private void upgrateStatsEvent()
    {
        if (playerLevel.levelUpStats(stasType))
        {
            statsCount++;
            statsUpgrateText.text = "+" + statsCount;
            Observer.Instance.NotifyEvent(GameEvent.UpdateUI, null);
            if (statsData.stats.ContainsKey(stasType))
            {
                statsData.stats[stasType] = statsCount;
            }
        }
    }
}
