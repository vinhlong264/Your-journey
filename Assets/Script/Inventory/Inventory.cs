using System.Collections.Generic;
using UnityEngine;
public class Inventory : Singleton<Inventory>
{

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

    [SerializeField] private Transform statsSlotParent; // Transform  Parent dùng để quản lý việc lưu và cập nhập UI
    [SerializeField] private UI_StatSlot[] statsSlot;

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
        itemIventoryList = new List<InventoryItem>();
        itemInvetoryDictionary = new Dictionary<itemDataSO, InventoryItem>();

        itemStashList = new List<InventoryItem>();
        itemStashDictionary = new Dictionary<itemDataSO, InventoryItem>();

        eqipmentItemList = new List<InventoryItem>();
        equipmentDictionary = new Dictionary<ItemEquipmentSO, InventoryItem>();

        itemIventorySLot = inventorySlotParent.GetComponentsInChildren<UI_ItemSlot>();
        itemStashSlot = stashSlotParent.GetComponentsInChildren<UI_ItemSlot>();
        equipmentSlot = equipmentSlotParent.GetComponentsInChildren<UI_EqipmentSlot>();
        statsSlot = statsSlotParent.GetComponentsInChildren<UI_StatSlot>();


        for (int i = 0; i < itemStart.Length; i++)
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

    #region Craft

    public void Craft(ItemEquipmentSO _eqipmentCratf , List<InventoryItem> _materialRequirement)
    {
        if(canCraft(_eqipmentCratf , _materialRequirement))
        {
            addEquipment(_eqipmentCratf);
            Debug.Log("Here is your item: " + _eqipmentCratf.name);
        }
    }

    private bool canCraft(ItemEquipmentSO _itemToCraft, List<InventoryItem> _requirmentMaterial) // Quản lý việc ghép nguyên liệu
    {
        if(_requirmentMaterial.Count <= 0) // kiểm tra xem Equipment này có yêu cầu material không
        {
            Debug.Log("Equipment not exits material");
            return false;
        }


        List<InventoryItem> materialToRemove = new List<InventoryItem>(); // List để chứa các material để rèn

        for (int i = 0; i < _requirmentMaterial.Count; i++)
        {
            //Tìm xem itemStashDictionary có material yêu cầu không, nếu không thì trả về false luôn và ngược lại
            if (itemStashDictionary.TryGetValue(_requirmentMaterial[i].itemData, out InventoryItem stashValue))
            {
                Debug.Log("Find material");

                // Nếu có, kiểm tra xem số lượng trong kho có đủ để rèn không
                if (stashValue.stackSize < _requirmentMaterial[i].stackSize)
                {
                    Debug.Log("Not enough material");
                    return false; // nếu không đủ thì trả về false luôn
                }
                else
                {
                    Debug.Log("Enough material");
                    materialToRemove.Add(stashValue);
                }
            }
            else
            {
                Debug.Log("Not Find material");
                return false;
            }
        }

        for (int i = 0; i < materialToRemove.Count; i++)
        {
            removeItem(materialToRemove[i].itemData); // xóa các material đã dùng để rèn đi
        }

        //addEquipment(_itemToCraft);
        //Debug.Log("Here is your item: " + _itemToCraft.name);
        return true;
    }

    #endregion

    private void updateSlotItemUI() // cập nhập các UI_item
    {
        for (int i = 0; i < equipmentSlot.Length; i++) // equipment
        {
            foreach (KeyValuePair<ItemEquipmentSO, InventoryItem> item in equipmentDictionary)
            {
                if (item.Key.EqipmentType == equipmentSlot[i].slotType)
                {
                    equipmentSlot[i].updateUISlotItem(item.Value);
                }
            }
        }


        for (int i = 0; i < itemIventorySLot.Length; i++) // Xóa các inventory item khi thay đổi
        {
            itemIventorySLot[i].cleanItem();
        }

        for (int i = 0; i < itemStashSlot.Length; i++) //  Xóa các stash item khi thay đổi
        {
            itemStashSlot[i].cleanItem();
        }

        //for(int i = 0; i < equipmentSlot.Length; i++)
        //{
        //    equipmentSlot[i].cleanItem();
        //}

        for (int i = 0; i < itemIventoryList.Count; i++) 
        {
            itemIventorySLot[i].updateUISlotItem(itemIventoryList[i]);
        }

        for (int i = 0; i < itemStashList.Count; i++)
        {
            itemStashSlot[i].updateUISlotItem(itemStashList[i]);
        }

        updateStatsUI();
    }

    public void updateStatsUI()
    {
        for (int i = 0; i < statsSlot.Length; i++)
        {
            statsSlot[i].updateStatsUI();
        }
    }

    #region add item
    public void addItem(itemDataSO _item)
    {
        if (_item.ItemType == ItemType.Equipment/* && canAddItem()*/)
        {
            //Debug.Log("Call");
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
        //Debug.Log("call");
        updateSlotItemUI();
    }

    public bool canAddItem()
    {
        if (itemIventoryList.Count >= itemIventorySLot.Length || itemStashList.Count >= itemStashSlot.Length)
        {
            Debug.Log("No more space");
            return false;
        }
        return true;
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


    #region take Eqipment and use skill special Equipment
    public ItemEquipmentSO getEquipmentBy(EqipmentType _type)
    {
        ItemEquipmentSO newEquipment = null;
        InventoryItem equipmentInven = null;
        foreach (KeyValuePair<ItemEquipmentSO, InventoryItem> item in equipmentDictionary)
        {
            if (item.Key.EqipmentType == _type)
            {
                newEquipment = item.Key;
                equipmentInven = item.Value;
            }
        }

        if(_type == EqipmentType.Bottle)
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

        updateSlotItemUI();
        lastTimeUseBollte = Time.time;
        newCurrentEffect.excuteItemEffect(null);
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
}


[System.Serializable]
public class InventoryItem // Class quản lý item trong Inventory
{
    public itemDataSO itemData; // SO
    public int stackSize; // số lượng

    public InventoryItem(itemDataSO _item)
    {
        itemData = _item;
        addStack();
    }

    public void addStack() => stackSize++; // thêm số lượng
    public void removeStack() => stackSize--; // giảm số lượng

}
