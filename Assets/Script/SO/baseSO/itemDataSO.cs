using System;
using System.Text;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "Data/Item")]
public class itemDataSO : ScriptableObject
{
    public ItemType ItemType;
    public string itemName;
    public Sprite icon;
    protected StringBuilder sb = new StringBuilder();
    [Range(0, 100)]
    public float rateDrop;

    public virtual string GetDescription()
    {
        return "";
    }
}

