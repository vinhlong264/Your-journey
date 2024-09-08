using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
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

    public void cleanItem()
    {
        item = null;
        imageItem.sprite = null;
        imageItem.color = Color.clear;
        itemText.text = "";
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if(item.data.ItemType == ItemType.Equipment)
        {
            Inventory.Instance.equipmentItem(item.data);
        }
    }
}
