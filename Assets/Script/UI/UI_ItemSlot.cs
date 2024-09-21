﻿using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class UI_ItemSlot : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image imageItem; // icon item
    [SerializeField] private TextMeshProUGUI itemText; // text số lượng

    [SerializeField] protected InventoryItem item; // Item


    public void updateUISlotItem(InventoryItem newItem) // Update item vào các slot
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

    public void cleanItem() // Clean Item
    {
        item = null;
        imageItem.sprite = null;
        imageItem.color = Color.clear;
        itemText.text = "";
    }

    public virtual void OnPointerDown(PointerEventData eventData) // trang bị Eqipment
    {
        if(item == null) return;

        if (Input.GetKeyDown(KeyCode.LeftControl)) // remove item in Iventory
        {
            Inventory.Instance.removeItem(item.data);
        }

        if(item.data.ItemType == ItemType.Equipment) // nếu cùng loại sẽ được thêm
        {
            Inventory.Instance.equipmentItem(item.data);
        }
    }
}
