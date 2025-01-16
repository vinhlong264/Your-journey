using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : Singleton<InventorySystem>, IInventory, IEquipable
{
    private Dictionary<itemDataSO, ItemInventory> inventoryDictionary = new Dictionary<itemDataSO, ItemInventory>();
    [SerializeField] private List<ItemInventory> inventoryList = new List<ItemInventory>();

    private Dictionary<ItemEquipmentSO, ItemInventory> equipmentDictionary = new Dictionary<ItemEquipmentSO, ItemInventory>();
    [SerializeField] private List<ItemInventory> equipmentList = new List<ItemInventory>();
    [SerializeField] private itemDataSO[] itemStart;

    private enum InventoryState
    {
        Start,
        End
    }

    private InventoryState state = InventoryState.Start;

    protected override void Awake()
    {
        MakeSingleton(true);
    }

    void Start()
    {
        for (int i = 0; i < itemStart.Length; i++)
        {
            addItem(itemStart[i]);
        }
    }

    #region Add Item
    public void addItem(itemDataSO _itemData)
    {
        state = InventoryState.Start;
        if (inventoryDictionary.TryGetValue(_itemData, out var _itemInvetory))
        {
            if (_itemData.onlyItem)
            {
                ItemInventory item = new ItemInventory(_itemData);
                inventoryList.Add(item);
            }
            else
            {
                _itemInvetory.addQuantitty(); 
            }
            state = InventoryState.End;
        }
        else
        {
            ItemInventory item = new ItemInventory(_itemData);
            inventoryDictionary.Add(_itemData, item);
            inventoryList.Add(item);

            state = InventoryState.End;
        }

        //Observer.Instance.NotifyEvent(GameEvent.UpdateUI, null);

        StartCoroutine(UpdatUI());
    }

    IEnumerator UpdatUI()
    {
        yield return new WaitUntil(() => state == InventoryState.End);

        Debug.Log("Cập nhập UI");
        Observer.Instance.NotifyEvent(GameEvent.UpdateUI, null);
        state = InventoryState.Start;
    }

    public void removeItem(itemDataSO _itemData)
    {
        if(inventoryDictionary.TryGetValue(_itemData, out ItemInventory value))
        {
            if (_itemData.onlyItem)
            {
                ItemInventory item = new ItemInventory(_itemData);
                inventoryList.Remove(item);
                inventoryDictionary.Remove(_itemData);
            }
            else
            {
                value.removeQuantity();
            }
        }
    }
    #endregion


    #region Equipment
    public void Equipment(ItemEquipmentSO _equipmentData)
    {
        ItemEquipmentSO newEquipment = _equipmentData;
        ItemInventory equipmentInInventory = new ItemInventory(_equipmentData);

        ItemEquipmentSO oldEquipment = null;

        if (equipmentDictionary.Count == 0)
        {
            Debug.Log("Hiện tại không có trang bị nào đang mặc");
            wearEquipment(newEquipment, equipmentInInventory);

            //inventoryList.Remove(equipmentInInventory);
            //inventoryDictionary.Remove(newEquipment);

            removeItem(newEquipment);
        }
        else
        {
            foreach (KeyValuePair<ItemEquipmentSO, ItemInventory> equipmentData in equipmentDictionary)
            {
                if (equipmentData.Key.EqipmentType == newEquipment.EqipmentType)
                {
                    Debug.Log("Lấy ra được equipment: " + equipmentData.Key.name);
                    oldEquipment = equipmentData.Key;
                }
            }

            if (oldEquipment != null)
            {
                unEquipment(oldEquipment);
            }

            equipmentList.Add(equipmentInInventory);
            equipmentDictionary.Add(newEquipment, equipmentInInventory);
            newEquipment.addModifier();

            addItem(_equipmentData);
        }
    }

    public void wearEquipment(ItemEquipmentSO newEquipment, ItemInventory equipmentInInventory)
    {
        equipmentList.Add(equipmentInInventory);
        equipmentDictionary.Add(newEquipment, equipmentInInventory);
        newEquipment.addModifier();
    }

    public void unEquipment(ItemEquipmentSO _equipmentData)
    {

        if (equipmentDictionary.TryGetValue(_equipmentData, out ItemInventory value)) // Tìm xem bên trong Dictionary có equipment cần tìm không
        {
            equipmentDictionary.Remove(_equipmentData);
            equipmentList.Remove(value);
            _equipmentData.removeModifier();

            addItem(_equipmentData);
        }
    }

    public Dictionary<itemDataSO , ItemInventory> GetDictionaryInventory()
    {
        Dictionary<itemDataSO, ItemInventory> GetData = new Dictionary<itemDataSO, ItemInventory>(inventoryDictionary);
        return GetData;
    }
    public List<ItemInventory> GetListInventory()
    {
        List<ItemInventory > GetData = new List<ItemInventory>(inventoryList);
        return GetData;
    }

    public Dictionary<ItemEquipmentSO , ItemInventory> GetDictionaryEquipment()
    {
        Dictionary<ItemEquipmentSO, ItemInventory> GetData = new Dictionary<ItemEquipmentSO, ItemInventory>(equipmentDictionary);
        return GetData;
    }
    public List<ItemInventory> GetListEquipment()
    {
        List<ItemInventory> GetData = new List<ItemInventory>(equipmentList);
        return GetData;
    }


    #endregion
}


[System.Serializable]
public class ItemInventory
{
    public itemDataSO _itemData;
    [SerializeField] private int currentQuantity;
    public int CurrentQuantity { get => currentQuantity; }

    public ItemInventory(itemDataSO itemData)
    {
        this._itemData = itemData;
        addQuantitty();
    }
    public void addQuantitty()
    {
        currentQuantity++;
    }

    public void removeQuantity()
    {
        currentQuantity--;
    }

}
