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

    private PlayerLevel playerLevel;

    void Start()
    {
        if (statsUpgrateText == null) return;

        UI_StatSlot = GetComponentInParent<UI_StatSlot>();
        playerLevel = GameManager.Instance.PlayerLevel;

        statsUpgrateText.text = "+" + statsCount;
        upgrateBtn.onClick.AddListener(() => upgrateStatsEvent());
    }
    public void LoadGame(GameData data)
    {
        if (data == null) return;

        if(data.stats.TryGetValue(stasType , out int value))
        {
            Debug.Log($"Key {stasType} - Value: {value}: ");
            statsCount = value;
        }
    }

    public void SaveGame(ref GameData data)
    {
        if(data.stats.TryGetValue(stasType , out int value))
        {
            data.stats.Remove(stasType);
            data.stats.Add(stasType, statsCount);
        }
        else
        {
            data.stats.Add(stasType, statsCount);
        }
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
