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
    void Start()
    {
        UI_StatSlot = GetComponent<UI_StatSlot>();

        statsUpgrateText.text = "+" + statsCount;
        upgrateBtn.onClick.AddListener(() => upgrateStatsEvent());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void upgrateStatsEvent()
    {
        if (PlayerManager.Instance.levelupAtribute(stasType))
        {
            statsCount++;
            statsUpgrateText.text = "+" + statsCount;
            /*Inventory.Instance.updateStatsUI();*/ // Cập nhập lại stats
        }
    }
}
