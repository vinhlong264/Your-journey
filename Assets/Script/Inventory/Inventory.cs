using System.Collections.Generic;
using UnityEngine;
public class Inventory : Singleton<Inventory>, IInventory
{
    [Header("Equipment infor")]
    [SerializeField] private List<ItemInventory> eqipmentItemList; // Danh sách Eqipment để thêm vào Eqipment table
    [SerializeField] private Dictionary<ItemEquipmentSO, ItemInventory> equipmentDictionary;

    [SerializeField] private List<ItemInventory> listInventory; // Danh sách item(Equipment) để thêm vào Inventory
    [SerializeField] private Dictionary<itemDataSO, ItemInventory> itemInvetoryDictionary;

    private float lastTimeUseBollte = 0;
    private float lastTimeUseArmor = 0;

    [SerializeField] private itemDataSO[] itemStart;

    public float LastTimeUseBollte { get => lastTimeUseBollte; }
    public float LastTimeUseArmor { get => lastTimeUseArmor; }

    protected override void Awake()
    {
        MakeSingleton(true);
    }

    private void Start()
    {
        listInventory = new List<ItemInventory>();
        itemInvetoryDictionary = new Dictionary<itemDataSO, ItemInventory>();

        eqipmentItemList = new List<ItemInventory>();
        equipmentDictionary = new Dictionary<ItemEquipmentSO, ItemInventory>();

        for (int i = 0; i < itemStart.Length; i++)
        {
            addItem(itemStart[i]);
        }
    }

    public void equipmentItem(itemDataSO _item) // Quản lý eqipment table
    {
        ItemEquipmentSO newEqipment = _item as ItemEquipmentSO;
        ItemInventory newItem = new ItemInventory(newEqipment);

        ItemEquipmentSO oldEqipment = null;

        foreach (KeyValuePair<ItemEquipmentSO, ItemInventory> item in equipmentDictionary) // Lấy ra key có Enum là EqipmentType 
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


        Observer.Instance.NotifyEvent(GameEvent.UpdateUI, null);
    }

    public void unEqipmentItem(ItemEquipmentSO eqipmentToRemove) // Quản lý việc gỡ trang bị
    {

        if (equipmentDictionary.TryGetValue(eqipmentToRemove, out ItemInventory value))
        {
            eqipmentItemList.Remove(value);
            equipmentDictionary.Remove(eqipmentToRemove);
            eqipmentToRemove.removeModifier();
        }

        Observer.Instance.NotifyEvent(GameEvent.UpdateUI, null);
    }
    #region add item
    public void addItem(itemDataSO _item)
    {
        if (itemInvetoryDictionary.TryGetValue(_item, out ItemInventory value))
        {
            if (_item.onlyItem)
            {
                listInventory.Add(value);
            }
            else
            {
                value.addQuantity();
            }
        }
        else
        {
            ItemInventory newItem = new ItemInventory(_item);
            listInventory.Add(newItem);
            itemInvetoryDictionary.Add(_item, newItem);
        }
        Observer.Instance.NotifyEvent(GameEvent.UpdateUI, null);
    }

    private void addEquipment(itemDataSO _item)
    {
        if (itemInvetoryDictionary.TryGetValue(_item, out ItemInventory value))
        {
            listInventory.Add(value);
        }
        else
        {
            ItemInventory newItem = new ItemInventory(_item);
            listInventory.Add(newItem);
            itemInvetoryDictionary.Add(_item, newItem);
        }
        //Debug.Log("call");
        //updateSlotItemUI();
    }

    public bool canAddItem()
    {
        return true;
    }

    #endregion

    #region remove item
    public void removeItem(itemDataSO _item)
    {

        if (itemInvetoryDictionary.TryGetValue(_item, out ItemInventory value))
        {
            if (_item.onlyItem)
            {
                listInventory.Remove(value);
                itemInvetoryDictionary.Remove(_item);
            }
            else
            {
                if (value.currentQuantity > 1)
                {
                    value.removeQuantity();
                }
                else
                {
                    listInventory.Remove(value);
                    itemInvetoryDictionary.Remove(_item);
                }
            }
        }
        Observer.Instance.NotifyEvent(GameEvent.UpdateUI, null);
    }

    private void removeItemInventory(itemDataSO _itemIventory)
    {
        if (itemInvetoryDictionary.TryGetValue(_itemIventory, out ItemInventory valueInventory))
        {
            if (valueInventory.currentQuantity <= 1) // nếu giá trị stacksize hiện tại bằng 1 thì sẽ xóa nó đi, nghĩa là có đúng 1 item loại key đó
            {
                listInventory.Remove(valueInventory);
                itemInvetoryDictionary.Remove(_itemIventory);
            }
            else // ngược lại nếu nhiều hơn 1 thì sẽ giảm số lượng item loại key đó sở hữu
            {
                valueInventory.removeQuantity(); // xóa 1 item cùng key khỏi Inventory
            }
        }
    }
    #endregion


    #region take Eqipment and use skill special Equipment
    public ItemEquipmentSO getEquipmentBy(EqipmentType _type)
    {
        ItemEquipmentSO newEquipment = null;
        ItemInventory equipmentInven = null;
        foreach (KeyValuePair<ItemEquipmentSO, ItemInventory> item in equipmentDictionary)
        {
            if (item.Key.EqipmentType == _type)
            {
                newEquipment = item.Key;
                equipmentInven = item.Value;
            }
        }

        if (_type == EqipmentType.Bottle)
        {
            equipmentDictionary.Remove(newEquipment);
            eqipmentItemList.Remove(equipmentInven);
        }

        return newEquipment;
    }

    public bool useCanBottle()
    {
        ItemEquipmentSO newCurrentEffect = getEquipmentBy(EqipmentType.Bottle);

        bool canUse = Time.time > lastTimeUseBollte + newCurrentEffect.coolDownEffect;
        if (!canUse)
        {
            Debug.Log("CoolDown");
            return false;
        }


        lastTimeUseBollte = Time.time;
        newCurrentEffect.excuteItemEffect(null);
        removeItem(newCurrentEffect);
        Debug.Log("Bật hiệu ứng");
        return true;
    }


    public bool canUseArmor()
    {
        ItemEquipmentSO currentArmor = getEquipmentBy(EqipmentType.Armor);

        if (!(Time.time > lastTimeUseArmor + currentArmor.coolDownEffect))
        {
            Debug.Log("Armor cooldown");
            return false;
        }

        Debug.Log("use");
        return true;
    }
    #endregion

    public List<ItemInventory> GetListInventory() => listInventory;
    public Dictionary<itemDataSO, ItemInventory> GetDictionaryInventory() => itemInvetoryDictionary;
    public Dictionary<ItemEquipmentSO, ItemInventory> GetDictionaryEqiupment() => equipmentDictionary;
}


[System.Serializable]
public class ItemInventory // Class quản lý item trong Inventory
{
    public itemDataSO itemData; // SO
    public int currentQuantity; // số lượng

    public ItemInventory(itemDataSO _item)
    {
        itemData = _item;
        addQuantity();
    }

    public void addQuantity() => currentQuantity++; // thêm số lượng
    public void removeQuantity() => currentQuantity--; // giảm số lượng

}
