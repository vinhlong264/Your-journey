using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image imageItem; // icon item
    [SerializeField] protected TextMeshProUGUI itemText; // text số lượng
    protected UI ui;
    [SerializeField] protected ItemInventory item; // Item

    protected virtual void Start()
    {
        ui = GetComponentInParent<UI>();
    }


    public virtual void updateUISlotItem(ItemInventory newItem) // Update item vào các slot
    {
        item = newItem;

        imageItem.color = Color.white;

        if (item != null)
        {
            imageItem.sprite = item.itemData.icon;
            if (item.currentQuantity > 1)
            {
                itemText.text = item.currentQuantity.ToString();
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

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null) return;

        ui.uiEquipmentInfo.showDescription(item.itemData as ItemEquipmentSO);
        ui.uiEquipmentInfo.transform.position = moveForMouse();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (item == null) return;

        ui.uiEquipmentInfo.hideDescription();
    }


    protected Vector3 moveForMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        float offSetX = 0;
        float offSetY = 0;

        if (mousePos.x > 600)
        {
            offSetX = -150f;
        }
        else
        {
            offSetX = 150f;
        }

        if (mousePos.y > 300)
        {
            offSetY = -150f;
        }
        else
        {
            offSetY = 150f;
        }

        return new Vector3(mousePos.x + offSetX, mousePos.y + offSetY, 0);
    }
}
