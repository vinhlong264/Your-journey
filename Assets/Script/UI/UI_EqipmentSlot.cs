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
        if (item == null || item.data == null) return;

        Inventory.Instance.unEqipmentItem(item.data as ItemEquipmentSO);
        Inventory.Instance.addItem(item.data as ItemEquipmentSO);
        cleanItem();
    }
}
