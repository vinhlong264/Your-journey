using System.Collections.Generic;
using UnityEngine;
public class Inventory : MonoBehaviour
{
    public static Inventory Instance { get; set; }

    [SerializeField] private List<InventoryItem> itemIventoryList;
    [SerializeField] private Dictionary<itemDataSO, InventoryItem> itemInvetoryDictionary;

    [Header("Iventory UI")]
    [SerializeField] private Transform itemSlotParent;
    [SerializeField] private UI_ItemSlot[] itemSlot;

    private void Awake()
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

        itemSlot = itemSlotParent.GetComponentsInChildren<UI_ItemSlot>();
    }

    private void updateSlotItemUI()
    {
        for(int i = 0; i < itemIventoryList.Count; i++)
        {
            itemSlot[i].updateUISlotItem(itemIventoryList[i]);
        }
    }

    public void addItem(itemDataSO _item)
    {
        if (itemInvetoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            value.addStack();
            Debug.Log("Nhận item có cùng key");
        }
        else
        {
            InventoryItem newItem = new InventoryItem(_item);
            itemIventoryList.Add(newItem);
            itemInvetoryDictionary.Add(_item, newItem);
            Debug.Log("Nhận item mới");
        }

        updateSlotItemUI();
    }

    public void removeItem(itemDataSO _item)
    {
        if (itemInvetoryDictionary.TryGetValue(_item, out InventoryItem value))
        {
            if (value.stackSize <= 1) // nếu giá trị stacksize hiện tại bằng 1 thì sẽ xóa nó đi, nghĩa là có đúng 1 item loại key đó
            {
                itemIventoryList.Remove(value);
                itemInvetoryDictionary.Remove(_item);
            }
            else // ngược lại nếu nhiều hơn 1 thì sẽ giảm số lượng item loại key đó sở hữu
            {
                value.removeStack(); // xóa 1 item cùng key khỏi Inventory
                Debug.Log($"Xóa 1 item có key: {_item} - value: {value}");
            }
        }
        updateSlotItemUI();
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            itemDataSO newItem = itemIventoryList[itemIventoryList.Count - 1].data;
            removeItem(newItem);
        }
    }

}


[System.Serializable]
public class InventoryItem
{
    public itemDataSO data;
    public int stackSize;

    public InventoryItem(itemDataSO _item)
    {
        data = _item;
        addStack();
    }

    public void addStack() => stackSize++;
    public void removeStack() => stackSize--;

}
