using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    
    public bool isUpdateStats;
    [SerializeField] private UI_StatsInfo _statsInfo;
    [SerializeField] string statName;
    [SerializeField] StatType statType;
    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statValue;
    [TextArea]
    [SerializeField] private string statDescription;

    private void OnValidate()
    {
        gameObject.name = "Stat - " + statName;

        if (statNameText != null)
        {
            statNameText.text = statName;
        }
    }


    protected void Start()
    {
        updateStatsUI();
    }

    private void Update()
    {

    }

    public void updateStatsUI()
    {
        PlayerStats playerStatus = PlayerManager.Instance.player.GetComponent<PlayerStats>();

        if (playerStatus == null) return;

        statValue.text = playerStatus.getStat(statType).getValue().ToString();

        parametersUpdate(playerStatus);
    }

    private void parametersUpdate(PlayerStats playerStatus)
    {
        if (statType == StatType.Health)
        {
            statValue.text = playerStatus.getMaxHealth().ToString();
        }

        if (statType == StatType.Armor)
        {
            statValue.text = (playerStatus.armor.getValue() + playerStatus.vitality.getValue() + 3).ToString();
        }

        if (statType == StatType.Dame)
        {
            statValue.text = (playerStatus.getMaxDame()).ToString();
        }

        if (statType == StatType.CritRate)
        {
            statValue.text = (playerStatus.critRate.getValue() + playerStatus.ability.getValue()).ToString() + "%";
        }

        if (statType == StatType.Evasion)
        {
            statValue.text = (playerStatus.evasion.getValue() + playerStatus.ability.getValue()).ToString() + "%";
        }

        if (statType == StatType.MagicResitance)
        {
            statValue.text = (playerStatus.magicResistance.getValue() + (playerStatus.inteligent.getValue() * 3)).ToString();
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (_statsInfo == null) return;

        _statsInfo.showStatsDes(statDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if ( _statsInfo == null) return;

        _statsInfo.hideStatsDes();
    }
}
