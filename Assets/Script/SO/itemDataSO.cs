using UnityEngine;

[CreateAssetMenu(fileName = "Data" , menuName = "Data/Item")]
public class itemDataSO : ScriptableObject
{
    public ItemType ItemType;
    public string itemName;
    public Sprite icon;
}

public enum ItemType
{
    Material,
    Equipment
}
