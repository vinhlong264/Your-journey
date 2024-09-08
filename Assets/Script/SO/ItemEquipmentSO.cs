
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Equipment")]
public class ItemEquipmentSO : itemDataSO
{
   public EqipmentType EqipmentType;
}
public enum EqipmentType
{
    Sword,
    Armor,
    Helmet,
    Bottle
}
