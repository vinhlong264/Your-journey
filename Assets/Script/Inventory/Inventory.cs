using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Inventory : Singleton<Inventory>, IInventory
{
    private enum InventoryState
    {
        Start,
        End
    }
    [SerializeField] private InventoryState _state = InventoryState.End;

    [Header("Equipment infor")]
    [SerializeField] private List<ItemInventory> eqipmentItemList; // Danh sách Eqipment để thêm vào Eqipment table
    [SerializeField] private Dictionary<ItemEquipmentSO, ItemInventory> equipmentDictionary;

    [SerializeField] private List<ItemInventory> listInventory; // Danh sách item(Equipment) để thêm vào Inventory
    [SerializeField] private Dictionary<itemDataSO, ItemInventory> itemInvetoryDictionary;

    [SerializeField] private List<ItemInventory> itemStashList;  //Danh sách item(Material) để thêm vào Stash
    [SerializeField] private Dictionary<itemDataSO, ItemInventory> itemStashDictionary;

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
        listInventory = new List<ItemInventory>();
        itemInvetoryDictionary = new Dictionary<itemDataSO, ItemInventory>();

        itemStashList = new List<ItemInventory>();
        itemStashDictionary = new Dictionary<itemDataSO, ItemInventory>();

        eqipmentItemList = new List<ItemInventory>();
        equipmentDictionary = new Dictionary<ItemEquipmentSO, ItemInventory>();

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
        _state = InventoryState.Start;
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

        //updateSlotItemUI();
        _state = InventoryState.End;

        StartCoroutine(UpdateUI());
    }

    public void unEqipmentItem(ItemEquipmentSO eqipmentToRemove) // Quản lý việc gỡ trang bị
    {
        if (equipmentDictionary.TryGetValue(eqipmentToRemove, out ItemInventory value))
        {
            eqipmentItemList.Remove(value);
            equipmentDictionary.Remove(eqipmentToRemove);
            eqipmentToRemove.removeModifier();
        }
    }

    #region Craft

    public void Craft(ItemEquipmentSO _eqipmentCratf , List<ItemInventory> _materialRequirement)
    {
        if(canCraft(_eqipmentCratf , _materialRequirement))
        {
            addEquipment(_eqipmentCratf);
            Debug.Log("Here is your item: " + _eqipmentCratf.name);
        }
    }

    private bool canCraft(ItemEquipmentSO _itemToCraft, List<ItemInventory> _requirmentMaterial) // Quản lý việc ghép nguyên liệu
    {
        if(_requirmentMaterial.Count <= 0) // kiểm tra xem Equipment này có yêu cầu material không
        {
            Debug.Log("Equipment not exits material");
            return false;
        }


        List<ItemInventory> materialToRemove = new List<ItemInventory>(); // List để chứa các material để rèn

        for (int i = 0; i < _requirmentMaterial.Count; i++)
        {
            //Tìm xem itemStashDictionary có material yêu cầu không, nếu không thì trả về false luôn và ngược lại
            if (itemStashDictionary.TryGetValue(_requirmentMaterial[i].itemData, out ItemInventory stashValue))
            {
                Debug.Log("Find material");

                // Nếu có, kiểm tra xem số lượng trong kho có đủ để rèn không
                if (stashValue.currentQuantity < _requirmentMaterial[i].currentQuantity)
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

        addItem(_itemToCraft);
        //Debug.Log("Here is your item: " + _itemToCraft.name);
        return true;
    }

    #endregion


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
        _state = InventoryState.Start;

        if(itemInvetoryDictionary.TryGetValue(_item , out ItemInventory value))
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
            itemInvetoryDictionary.Add(_item , newItem);
        }

        _state = InventoryState.End;

        StartCoroutine(UpdateUI());
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
        if (listInventory.Count >= itemIventorySLot.Length || itemStashList.Count >= itemStashSlot.Length)
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

        //updateSlotItemUI();
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

    private void removeItemStash(itemDataSO _itemStash)
    {
        if (itemStashDictionary.TryGetValue(_itemStash, out ItemInventory valueStash))
        {
            if (valueStash.currentQuantity <= 1)
            {
                itemStashList.Remove(valueStash);
                itemStashDictionary.Remove(_itemStash);
            }
            else
            {
                valueStash.removeQuantity();
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

    IEnumerator UpdateUI()
    {
        yield return new WaitUntil(() => _state == InventoryState.End);

        Observer.Instance.NotifyEvent(GameEvent.UpdateUI, null);

        _state = InventoryState.Start;
    }


    public List<ItemInventory> GetListInventory() => listInventory;
    public Dictionary<ItemEquipmentSO , ItemInventory> GetDictionanryEqiupment() => equipmentDictionary;
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
