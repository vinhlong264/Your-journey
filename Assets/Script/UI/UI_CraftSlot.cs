using UnityEngine.EventSystems;

public class UI_CraftSlot : UI_ItemSlot
{
    protected override void Awake()
    {
        base.Awake();
    }

    public void setUpCraftSlot(ItemEquipmentSO _data) // Cài đặt những thứ cần thiết
    {
        if (_data == null) return; 


        item.data = _data;

        imageItem.sprite = _data.icon;
        itemText.text = _data.name;
    }


    public override void OnPointerDown(PointerEventData eventData)
    {
        ItemEquipmentSO itemCraftData = item.data as ItemEquipmentSO;
        if(Inventory.Instance.canCraft(itemCraftData, itemCraftData.craft)) // kiểm tra xem có đủ điều kiện để Craft không
        {
            Inventory.Instance.addItem(itemCraftData);
        }
    }
}
