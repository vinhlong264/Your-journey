using UnityEngine.EventSystems;

public class UI_Craft : UI_ItemSlot
{

    private void OnEnable()
    {
        updateUISlotItem(item);
    }
    public override void OnPointerDown(PointerEventData eventData)
    {
        ItemEquipmentSO itemCraftData = item.data as ItemEquipmentSO;
        Inventory.Instance.canCraft(itemCraftData, itemCraftData.craft); 
    }
}
