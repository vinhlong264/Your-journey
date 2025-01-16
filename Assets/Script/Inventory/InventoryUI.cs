using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [Header("Item slot UI")]
    [SerializeField] private Transform itemManagerParent; // Transform Parent dùng để quản lý các slot Item(Equipment)
    [SerializeField] private UI_ItemSlot[] itemSLot;

    [Header("Eqiupment slot UI")]
    [SerializeField] private Transform equipmentManagerParent;
    [SerializeField] private UI_EqipmentSlot[] equipmentSLot;

    [Header("Stash slot UI")]
    [SerializeField] private Transform stashManagerParent;
    [SerializeField] private UI_StatSlot[] statSlot;


    [SerializeField] List<ItemInventory> items = new List<ItemInventory>();

    private void OnEnable()
    {
        Observer.Instance.subscribeListener(GameEvent.UpdateUI, updateInventoryUI);
    }

    private void OnDisable()
    {
        if(Observer.Instance != null)
        {
            Observer.Instance.unsubscribeListener(GameEvent.UpdateUI, updateInventoryUI);
        }
    }

    void Start()
    {
        itemSLot = itemManagerParent.GetComponentsInChildren<UI_ItemSlot>();

        equipmentSLot = equipmentManagerParent.GetComponentsInChildren<UI_EqipmentSlot>();

        //statSlot = stashManagerParent.GetComponentsInChildren<UI_StatSlot>();
    }

    private void updateInventoryUI(object value)
    {
        items = InventorySystem.Instance.GetListInventory();


       for(int i = 0; i < items.Count; i++)
        {
            itemSLot[i].updateUISlotItem(items[i]);
        }
    }
}
