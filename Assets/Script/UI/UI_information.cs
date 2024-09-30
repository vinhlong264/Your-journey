using TMPro;
using UnityEngine;

public class UI_information : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI nameItemText;
    [SerializeField] private TextMeshProUGUI typeItemText;
    [SerializeField] private TextMeshProUGUI descriptionText;
    private int frontSizeDefaunt = 36;



    public void showDescription(ItemEquipmentSO itemEqipment)
    {
        if (itemEqipment == null) return;


        nameItemText.text = itemEqipment.name;
        typeItemText.text = itemEqipment.EqipmentType.ToString();
        descriptionText.text = itemEqipment.GetDescription();

        adjustFrontSize(nameItemText);

        gameObject.SetActive(true);
    }

    public void hideDescription()
    {
        gameObject.SetActive(false);
    }


    private void adjustFrontSize(TextMeshProUGUI _text)
    {
        if(_text.text.Length > 15)
        {
            _text.fontSize *= 0.8f;
        }
        else
        {
            _text.fontSize = frontSizeDefaunt;
        }
    }
}
