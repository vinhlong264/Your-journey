using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftSystem : MonoBehaviour
{

    public void Craft(ItemEquipmentSO _equipmentCraft , List<ItemInventory> materialRequirements)
    {
        if(canCraft(_equipmentCraft , materialRequirements))
        {
            Inventory.Instance.addItem(_equipmentCraft);
        }
    }

    private bool canCraft(ItemEquipmentSO _equipmentCraft , List<ItemInventory> _listRequireMaterial)
    {
        if(_equipmentCraft == null) return false;
        if (_listRequireMaterial.Count < 0) return false;

        Dictionary<itemDataSO, ItemInventory> inventoryDump = new Dictionary<itemDataSO, ItemInventory>(Inventory.Instance.GetDictionaryInventory());

        List<ItemInventory> listMaterialCraft = new List<ItemInventory>();
        
        for(int i = 0; i < _listRequireMaterial.Count; i++)
        {
            if (inventoryDump.TryGetValue(listMaterialCraft[i].itemData , out ItemInventory value))
            {
                Debug.Log("Find material: "+listMaterialCraft[i].itemData);

                if(value.currentQuantity < listMaterialCraft[i].currentQuantity)
                {
                    Debug.Log("Not enough material");
                    return false;
                }
                else
                {
                    Debug.Log("Enough material");
                    listMaterialCraft.Add(value);
                    return true;
                }

            }
            else
            {
                Debug.Log("Not find material");
                return false;
            }
        }

        foreach(var item in listMaterialCraft)
        {
            Inventory.Instance.removeItem(item.itemData);
        }
        return true ;
    }
}
