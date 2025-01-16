public interface IEquipable
{
    void Equipment(ItemEquipmentSO equipmentSO);
    void wearEquipment(ItemEquipmentSO newEquipment, ItemInventory equipmentInInventory);
    void unEquipment(ItemEquipmentSO equipmentSO);
}
