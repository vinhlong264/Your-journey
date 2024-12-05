using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_StatSlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] string statName;
    [SerializeField] StatType statType;
    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statValue;
    [SerializeField] UI uiDes;
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


    void Start()
    {
        updateStatusUI();
        uiDes = GetComponentInParent<UI>();
    }


    public void updateStatusUI()
    {
        PlayerStatus playerStatus = PlayerManager.Instance.player.GetComponent<PlayerStatus>();

        if (playerStatus == null) return;

        statValue.text = playerStatus.getStat(statType).getValue().ToString();

        parametersUpdate(playerStatus);
    }

    private void parametersUpdate(PlayerStatus playerStatus)
    {
        if (statType == StatType.Health)
        {
            statValue.text = playerStatus.getMaxHealth().ToString();
        }

        if (statType == StatType.Dame)
        {
            statValue.text = (playerStatus.dame.getValue() + playerStatus.strength.getValue()).ToString();
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
        uiDes.uiStatsInfo.showStatsDes(statDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        uiDes.uiStatsInfo.hideStatsDes();
    }
}
