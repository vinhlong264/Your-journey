﻿using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; set; }

    [Header("Equipment infor")]
    [SerializeField] private List<InventoryItem> eqipmentItemList; // Danh sách Eqipment để thêm vào Eqipment table
    [SerializeField] private Dictionary<ItemEquipmentSO, InventoryItem> equipmentDictionary;

    [SerializeField] private List<InventoryItem> itemIventoryList; // Danh sách item(Equipment) để thêm vào Inventory
    [SerializeField] private Dictionary<itemDataSO, InventoryItem> itemInvetoryDictionary;

    [SerializeField] private List<InventoryItem> itemStashList;  //Danh sách item(Material) để thêm vào Stash
    [SerializeField] private Dictionary<itemDataSO, InventoryItem> itemStashDictionary;

    [Header("Iventory UI")]
    [SerializeField] private Transform inventorySlotParent; // Transform Parent dùng để quản lý các slot Item(Equipment)
    [SerializeField] private UI_ItemSlot[] itemIventorySLot;

    [SerializeField] private Transform stashSlotParent; // Transform Parent dùng để quản lý các slot Item(Material)
    [SerializeField] private UI_ItemSlot[] itemStashSlot;

    [SerializeField] private Transform equipmentSlotParent; // Transform Parent dùng để quản lý việc lưu vào slot Eqipment table
    [SerializeField] private UI_EqipmentSlot[] equipmentSlot;



    [SerializeField] private itemDataSO[] itemStart;

    private void Awake() // Singleton
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        itemIventoryList = new List<InventoryItem>();
        itemInvetoryDictionary = new Dictionary<itemDataSO, InventoryItem>();

        itemStashList = new List<InventoryItem>();
        itemStashDictionary = new Dictionary<itemDataSO, InventoryItem>();

        eqipmentItemList = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemEquipmentSO, InventoryItem>();

        itemIventorySLot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        itemStashSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EqipmentSlot>();


        for(int i = 0; i < itemStart.Length; i++)
        {
            addItem(itemStart[i]);
        }
    }

    public void equipmentItem(itemDataSO _item) // Quản lý eqipment table
    {
        ItemEquipmentSO newEqipment = _item as ItemEquipmentSO;
        InventoryItem newItem = new InventoryItem(newEqipment);

        ItemEquipmentSO oldEqipment = null;

        foreach (KeyValuePair<ItemEquipmentSO, InventoryItem> item in equipmentDictionary) // Lấy ra key có Enum là EqipmentType 
        {
            if (item.Key.EqipmentType == newEqipment.EqipmentType)
            {
                oldEqipment = item.Key;
            }
        }

        if (oldEqipment != null) // nếu nó tồn tại sẽ thay thế nó bằng 1 equipment mới
        {
            unEqipmentItem(oldEqipment);
            addItem(oldEqipment);
        }

        eqipmentItemList.Add(newItem); // thêm eqipment vào bảng trang bị
        equipmentDictionary.Add(newEqipment, newItem); // add vào dictionary

        newEqipment.addModifier(); // Thay đổi thông số
        removeItem(_item);// xóa item này khỏi list Iventory

        updateSlotItemUI();
    }

    public void unEqipmentItem(ItemEquipmentSO eqipmentToRemove) // Quản lý việc gỡ trang bị
    {
        if (equipmentDictionary.TryGetValue(eqipmentToRemove, out InventoryItem value))
        {
            eqipmentItemList.Remove(value);
            equipmentDictionary.Remove(eqipmentToRemove);
            eqipmentToRemove.removeModifier();
        }
    }

    public bool canCraft(ItemEquipmentSO _itemToCraft , List<InventoryItem> _requirmentMaterial) // Quản lý việc ghép nguyên liệu
    {
        List<InventoryItem> materialToRemove = new List<InventoryItem>();
        for(int i = 0; i < _requirmentMaterial.Count ; i++)
        {
            if (itemStashDictionary.TryGetValue(_requirmentMaterial[i].data, out InventoryItem stashValue))
            {
                if(stashValue.stackSize < _requirmentMaterial[i].stackSize)
                {
                    Debug.Log("Not enough material");
                    return false;
                }
                else
                {
                    materialToRemove.Add(stashValue);
                }
            }
            else
            {
                Debug.Log("Not enough material");
                return false;
            }
        }

        for(int i = 0;i < materialToRemove.Count; i++)
        {
            removeItem(materialToRemove[i].data);
        }
        Debug.Log("Here is your item: "+ _itemToCraft.name);    
        return true;
    }

    private void updateSlotItemUI()
    {
        for (int i = 0; i < equipmentSlot.Length; i++)
        {
            foreach (KeyValuePair<ItemEquipmentSO, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.EqipmentType == equipmentSlot[i].slotType)
                {
                    equipmentSlot[i].updateUISlotItem(item.Value);
                }
            }
        }


        for (int i = 0; i < itemIventorySLot.Length; i++)
        {
            itemIventorySLot[i].cleanItem();
        }

        for (int i = 0; i < itemStashSlot.Length; i++)
        {
            itemStashSlot[i].cleanItem();
        }

        for (int i = 0; i < itemIventoryList.Count; i++)
        {
            itemIventorySLot[i].updateUISlotItem(itemIventoryList[i]);
        }

        for (int i = 0; i < itemStashList.Count; i++)
        {
            itemStashSlot[i].updateUISlotItem(itemStashList[i]);
        }
    }

    #region add item
    public void addItem(itemDataSO _item)
    {
        if (_item.ItemType == ItemType.Equipment)
        {
            addEquipment(_item);
        }
        else if (_item.ItemType == ItemType.Material)
        {
            addMaterial(_item);
        }

        updateSlotItemUI();
    }
    private void addMaterial(itemDataSO _item)
    {
        if (itemStashDictionary.TryGetValue(_item, out InventoryItem stashValue))
        {
            stashValue.addStack();
        }
        else
        {
            InventoryItem newItemStash = new InventoryItem(_item);
            itemStashList.Add(newItemStash);
            itemStashDictionary.Add(_item, newItemStash);
        }
    }

    private void addEquipment(itemDataSO _item)
    {
        if (itemInvetoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.addStack();
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            itemIventoryList.Add(newItem);
            itemInvetoryDictionary.Add(_item, newItem);
        }
    }
    #endregion

    #region remove item
    public void removeItem(itemDataSO _item)
    {
        removeItemInventory(_item);

        removeItemStash(_item);

        updateSlotItemUI();
    }

    private void removeItemInventory(itemDataSO _itemIventory)
    {
        if (itemInvetoryDictionary.TryGetValue(_itemIventory, out InventoryItem valueInventory))
        {
            if (valueInventory.stackSize <= 1) // nếu giá trị stacksize hiện tại bằng 1 thì sẽ xóa nó đi, nghĩa là có đúng 1 item loại key đó
            {
                itemIventoryList.Remove(valueInventory);
                itemInvetoryDictionary.Remove(_itemIventory);
            }
            else // ngược lại nếu nhiều hơn 1 thì sẽ giảm số lượng item loại key đó sở hữu
            {
                valueInventory.removeStack(); // xóa 1 item cùng key khỏi Inventory
            }
        }
    }

    private void removeItemStash(itemDataSO _itemStash)
    {
        if (itemStashDictionary.TryGetValue(_itemStash, out InventoryItem valueStash))
        {
            if (valueStash.stackSize <= 1)
            {
                itemStashList.Remove(valueStash);
                itemStashDictionary.Remove(_itemStash);
            }
            else
            {
                valueStash.removeStack();
            }
        }
    }
    #endregion

    public ItemEquipmentSO getEquipmentBy(EqipmentType _type)
    {
        ItemEquipmentSO newEquipment = null;
        foreach(KeyValuePair<ItemEquipmentSO , InventoryItem> item in equipmentDictionary)
        {
            if(item.Key.EqipmentType == _type)
            {
                newEquipment = item.Key;
            }
        }

        return newEquipment;
    }

}


[System.Serializable]
public class InventoryItem // Class quản lý item trong Inventory
{
    public itemDataSO data; // SO
    public int stackSize; // số lượng

    public InventoryItem(itemDataSO _item)
    {
        data = _item;
        addStack();
    }

    public void addStack() => stackSize++; // thêm số lượng
    public void removeStack() => stackSize--; // giảm số lượng

}
