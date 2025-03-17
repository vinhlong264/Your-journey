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
    [SerializeField] private LevelSO statsData;

    private PlayerLevel playerLevel;

    private void OnEnable()
    {
        SaveManager.Instance.addSubISave(this);
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

        if(statsData.stats.TryGetValue(stasType , out int value))
        {
            statsCount = value;
        }
    }

    public void SaveGame(ref GameData data)
    {
        if(statsData.stats.TryGetValue(stasType, out int value))
        {
            data.stats[stasType] = value;
        }
    }

    void Start()
    {
        UI_StatSlot = GetComponentInParent<UI_StatSlot>();
        playerLevel = GameManager.Instance.playerLevel;

        statsUpgrateText.text = "+" + statsCount;
        upgrateBtn.onClick.AddListener(() => upgrateStatsEvent());
    }


    private void upgrateStatsEvent()
    {
        if (playerLevel.levelUpStats(stasType))
        {
            statsCount++;
            statsUpgrateText.text = "+" + statsCount;
            Observer.Instance.NotifyEvent(GameEvent.UpdateUI, null);
        }
    }
}
