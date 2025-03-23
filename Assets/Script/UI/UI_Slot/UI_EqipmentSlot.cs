using UnityEngine.EventSystems;

public class UI_EqipmentSlot : UI_ItemSlot
{
    public EqipmentType slotType;

    protected override void Start()
    {
        base.Start();
    }

    private void OnValidate()
    {
        gameObject.name = slotType.ToString();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (item == null || item.itemData == null) return;

        if (slotType != EqipmentType.Bottle)
        {
            inventory.unEqipmentItem(item.itemData as ItemEquipmentSO);
            inventory.addItem(item.itemData as ItemEquipmentSO);
        }
        cleanItem();
    }
}
