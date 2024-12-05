using UnityEngine.EventSystems;

public class UI_EqipmentSlot : UI_ItemSlot
{
    public EqipmentType slotType;

    private void OnValidate()
    {
        gameObject.name = slotType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.itemData == null) return;

        Inventory.Instance.unEqipmentItem(item.itemData as ItemEquipmentSO);
        Inventory.Instance.addItem(item.itemData as ItemEquipmentSO);
        cleanItem();
    }
}