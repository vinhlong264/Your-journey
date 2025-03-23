using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] protected Image imageItem; // icon item
    [SerializeField] protected TextMeshProUGUI itemText; // text số lượng
    [SerializeField] protected UI_EqipmentInfor equipmentInfor;
    [SerializeField] protected ItemInventory item; // Item
    protected Inventory inventory;

    protected virtual void Start()
    {
        inventory = GameManager.Instance.Inventory;
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

        Debug.Log(item);

        if (Input.GetKeyDown(KeyCode.LeftControl)) // remove item in Iventory
        {
            inventory.removeItem(item.itemData);
        }

        if (item.itemData.ItemType == ItemType.Equipment) // nếu cùng loại sẽ được thêm
        {
            Debug.Log("Call");
            inventory.equipmentItem(item.itemData);
        }
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        if (item == null) return;

        equipmentInfor.showDescription(item.itemData as ItemEquipmentSO);
        equipmentInfor.transform.position = moveForMouse();
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        if (item == null) return;
        equipmentInfor.hideDescription();
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
