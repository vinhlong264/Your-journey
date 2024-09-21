using TMPro;
using UnityEngine;

public class UI_StatSlot : MonoBehaviour
{
    [SerializeField] string statName;
    [SerializeField] StatType statType;
    [SerializeField] TextMeshProUGUI statNameText;
    [SerializeField] TextMeshProUGUI statValue;

    private void OnValidate()
    {
        gameObject.name = "Stat - "+statName;

        if(statNameText != null)
        {
            statNameText.text = statName;
        }
    }


    void Start()
    {
        updateStatusUI();
    }


    public void updateStatusUI()
    {
        PlayerStatus playerStatus = PlayerManager.Instance.player.GetComponent<PlayerStatus>();

        if(playerStatus != null)
        {
            statValue.text = playerStatus.getStat(statType).getValue().ToString();
        }
    }

    
}
