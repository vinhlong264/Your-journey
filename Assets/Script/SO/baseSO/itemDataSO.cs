using System;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Item")]
public class itemDataSO : ScriptableObject
{
    public ItemType ItemType;
    public string itemName;
    public Sprite icon;
    [Range(0, 100)]
    public float rateDrop; // tỉ lệ rơi của item
}

//public enum ItemType
//{
//    Material,
//    Equipment
//}
