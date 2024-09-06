using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ItemSlot : MonoBehaviour
{
    [SerializeField] private Image imageItem;
    [SerializeField] private TextMeshProUGUI itemText;

    [SerializeField] private InventoryItem item;
    
    public void updateUISlotItem(InventoryItem newItem)
    {
        item = newItem;

        imageItem.color = Color.white;
        if(item != null)
        {
            imageItem.sprite = item.data.icon;
            if(item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    
}
