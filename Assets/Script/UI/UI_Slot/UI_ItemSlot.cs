using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image imageItem; // icon item
    [SerializeField] protected TextMeshProUGUI itemText; // text số lượng

    [SerializeField] public InventoryItem item; // Item

    [SerializeField] protected UI uiDes;


    protected virtual void Awake()
    {
        uiDes = GetComponentInParent<UI>();
    }


    public void updateUISlotItem(InventoryItem newItem) // Update item vào các slot
    {
        item = newItem;

        imageItem.color = Color.white;
        if (item != null)
        {
            imageItem.sprite = item.itemData.icon;
            if (item.stackSize > 1)
            {
                itemText.text = item.stackSize.ToString();
            }
            else
            {
                itemText.text = "";
            }
        }
    }

    public void cleanItem() // Clean Item
    {
        item = null;
        imageItem.sprite = null;
        imageItem.color = Color.clear;
        itemText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData) // trang bị Eqipment
    {
        if (item == null) return;

        if (Input.GetKeyDown(KeyCode.LeftControl)) // remove item in Iventory
        {
            Inventory.Instance.removeItem(item.itemData);
        }

        if (item.itemData.ItemType == ItemType.Equipment) // nếu cùng loại sẽ được thêm
        {
            Inventory.Instance.equipmentItem(item.itemData);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null) return;

        uiDes.uiEquipmentInfo.showDescription(item.itemData as ItemEquipmentSO);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (item == null) return;

        uiDes.uiEquipmentInfo.hideDescription();
    }
}
