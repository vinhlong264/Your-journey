using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    protected override void Start()
    {
        base.Start();
    }

    public void setUpCraftSlot(ItemEquipmentSO _data) // Cài đặt những thứ cần thiết
    {
        if (_data == null) return; 


        item._itemData = _data;

        imageItem.sprite = _data.icon;
        itemText.text = _data.name;
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        ui.uiCanCraftWindow.setUpCraftWindow(item._itemData as ItemEquipmentSO);
    }
}
