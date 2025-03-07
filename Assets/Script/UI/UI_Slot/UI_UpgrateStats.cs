using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_UpgrateStats : MonoBehaviour
{
    [SerializeField] private StatType stasType;
    [SerializeField] private Button upgrateBtn;
    [SerializeField] private TextMeshProUGUI statsUpgrateText;
    [SerializeField] private int statsCount;
    [SerializeField] private UI_StatSlot UI_StatSlot;

    private PlayerLevel playerLevel;
    void Start()
    {
        UI_StatSlot = GetComponentInParent<UI_StatSlot>();
        playerLevel = GameManager.Instance.playerLevel;

        statsUpgrateText.text = "+" + statsCount;
        upgrateBtn.onClick.AddListener(() => upgrateStatsEvent());
    }


    private void upgrateStatsEvent()
    {
        //if (playerLevel.canLevelUpStats(stasType))
        //{
        //    statsCount++;
        //    statsUpgrateText.text = "+" + statsCount;
        //    Observer.Instance.NotifyEvent(GameEvent.UpdateUI, null);
        //}
    }
}
