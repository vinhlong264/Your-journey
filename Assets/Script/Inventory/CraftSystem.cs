using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{
    public void Craft(ItemEquipmentSO newEquipemt)
    {
        if (newEquipemt == null) return;

        if (CanCraft(newEquipemt.craft))
        {
            Inventory.Instance.addItem(newEquipemt);
        }
    }

    private bool CanCraft(List<ItemInventory> necessaryMaterials)
    {
        if (necessaryMaterials.Count == 0) return false;

        List<ItemInventory> listTakeMaterial = new List<ItemInventory>();
        Dictionary<itemDataSO, ItemInventory> inventoryDump = new Dictionary<itemDataSO, ItemInventory>(Inventory.Instance.GetDictionaryInventory());

        for (int i = 0; i < necessaryMaterials.Count; i++)
        {
            if (inventoryDump.TryGetValue(necessaryMaterials[i].itemData, out ItemInventory value))
            {
                if (value.currentQuantity < necessaryMaterials[i].currentQuantity)
                {
                    Debug.Log("Not enough material to craft");
                    return false;
                }
                Debug.Log("Enough material to craft");
                listTakeMaterial.Add(value);
            }
            else
            {
                Debug.Log("Not find material in Inventory");
                return false;
            }
        }

        foreach (var item in listTakeMaterial) // xóa các item material bên trong inventory khi đã lấy ra để craft
        {
            Inventory.Instance.removeItem(item.itemData);
        }


        return true;
    }
}
