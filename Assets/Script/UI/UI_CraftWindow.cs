using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_CraftWindow : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI itemDescription;
    [SerializeField] private Image itemIcon;

    [SerializeField] private Image[] materialImage;
    [SerializeField] private Button caftButton;
    [SerializeField] private CraftSystem craftSystem;


    public void setUpCraftWindow(ItemEquipmentSO _data)
    {
        caftButton.onClick.RemoveAllListeners();

        for (int i = 0; i < materialImage.Length; i++)
        {
            materialImage[i].color = Color.clear;
            materialImage[i].GetComponentInChildren<TextMeshProUGUI>().color = Color.clear;
        }

        for (int i = 0; i < _data.craft.Count; i++)
        {
            if (_data.craft.Count > materialImage.Length)
            {
                Debug.Log("You have more material amount than you have material slot is craft window");
            }


            materialImage[i].sprite = _data.craft[i].itemData.icon;
            materialImage[i].color = Color.white;

            TextMeshProUGUI materialText = materialImage[i].GetComponentInChildren<TextMeshProUGUI>();

            if (materialText != null)
            {
                materialText.text = _data.craft[i].currentQuantity.ToString();
                materialText.color = Color.white;
            }
        }

        itemIcon.sprite = _data.icon;
        itemName.text = _data.itemName;
        itemDescription.text = _data.GetDescription();

        caftButton.onClick.AddListener(() => craftSystem.Craft(_data));
    }
}
