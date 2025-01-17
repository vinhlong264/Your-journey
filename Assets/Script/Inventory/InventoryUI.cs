using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("UI Inventory")]
    [SerializeField] private Transform inventoryParent;
    [SerializeField] private UI_ItemSlot[] itemSlots;
    private List<ItemInventory> inventoryItems;

    [Header("UI equippable")]
    [SerializeField] private Transform equippableParent;
    [SerializeField] private UI_EqipmentSlot[] equippableSlots;
    private Dictionary<ItemEquipmentSO, ItemInventory> equipmentItems;

    private void OnEnable()
    {
        Observer.Instance.subscribeListener(GameEvent.UpdateUI, updateUI);
    }

    private void OnDisable()
    {
        Observer.Instance.unsubscribeListener(GameEvent.UpdateUI, updateUI);
    }

    private void Awake()
    {
        itemSlots = inventoryParent.GetComponentsInChildren<UI_ItemSlot>();
        equippableSlots = equippableParent.GetComponentsInChildren<UI_EqipmentSlot>();
    }

    private void updateUI(object value)
    {
        inventoryItems = Inventory.Instance.GetListInventory();
        equipmentItems = Inventory.Instance.GetDictionanryEqiupment();

        for (int i = 0; i < equippableSlots.Length; i++)
        {
            foreach (KeyValuePair<ItemEquipmentSO, ItemInventory> e in equipmentItems)
            {
                if (e.Key.EqipmentType == equippableSlots[i].slotType)
                {
                    Debug.Log("Equipment: " + e.Key.EqipmentType);
                    equippableSlots[i].updateUISlotItem(e.Value);
                }
            }
        }

        for (int i = 0; i < itemSlots.Length; i++)
        {
            itemSlots[i].cleanItem();
        }



        for (int i = 0; i < inventoryItems.Count; i++)
        {
            itemSlots[i].updateUISlotItem(inventoryItems[i]);
        }
    }
}
