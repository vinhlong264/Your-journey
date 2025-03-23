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

    [Header("UI Stats")]
    [SerializeField] private Transform statsParent;
    [SerializeField] private UI_StatSlot[] statsSlots;

    private Inventory inventory;

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
        statsSlots = statsParent.GetComponentsInChildren<UI_StatSlot>();
    }

    private void Start()
    {
        inventory = GameManager.Instance.Inventory;
    }

    private void updateUI(object value)
    {
        inventoryItems = inventory.GetListInventory();
        equipmentItems = inventory.GetDictionaryEqiupment();

        for (int i = 0; i < equippableSlots.Length; i++)
        {
            foreach (KeyValuePair<ItemEquipmentSO, ItemInventory> e in equipmentItems)
            {
                if (equippableSlots[i].slotType == e.Key.EqipmentType)
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

        for(int i = 0; i < statsSlots.Length; i++)
        {
            statsSlots[i].updateStatsUI();
        }
    }
}
