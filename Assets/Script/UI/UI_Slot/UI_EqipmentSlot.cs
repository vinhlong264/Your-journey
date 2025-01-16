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
        if (item == null || item._itemData == null) return;

        Inventory.Instance.unEqipmentItem(item._itemData as ItemEquipmentSO);
        Inventory.Instance.addItem(item._itemData as ItemEquipmentSO);
        cleanItem();
    }
}
