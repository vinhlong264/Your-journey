using System.Collections.Generic;
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

    private void OnEnable()
    {
        Observer.Instance.subscribeListener(GameEvent.UpdateUI, updateInventoryUI);
    }

    private void OnDisable()
    {
        Observer.Instance.unsubscribeListener(GameEvent.UpdateUI , updateInventoryUI);
    }

    void Start()
    {
        itemSLot = itemManagerParent.GetComponentsInChildren<UI_ItemSlot>();

        equipmentSLot = equipmentManagerParent.GetComponentsInChildren<UI_EqipmentSlot>();

        //statSlot = stashManagerParent.GetComponentsInChildren<UI_StatSlot>();
    }

    private void updateInventoryUI(object value)
    {
        for (int i = 0; i < equipmentSLot.Length; i++) // equipment
        {
            foreach (KeyValuePair<ItemEquipmentSO, InventoryItem> item in Inventory.Instance.GetDictionaryEquiment())
            {
                if (item.Key.EqipmentType == equipmentSLot[i].slotType)
                {
                    //Debug.Log(item.Value);
                    equipmentSLot[i].updateUISlotItem(item.Value);
                }
            }
        }


        for (int i = 0; i < itemSLot.Length; i++) // Xóa các inventory item khi thay đổi
        {
            itemSLot[i].cleanItem();
        }


        for (int i = 0; i < equipmentSLot.Length; i++)
        {
            equipmentSLot[i].cleanItem();
        }

        for (int i = 0; i < Inventory.Instance.GetListItem().Count; i++)
        {
            itemSLot[i].updateUISlotItem(Inventory.Instance.GetListItem()[i]);
        }
    }
}
