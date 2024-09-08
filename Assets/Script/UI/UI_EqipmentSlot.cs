using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EqipmentSlot : UI_ItemSlot
{
    public EqipmentType slotType;

    private void OnValidate()
    {
        gameObject.name = slotType.ToString();
    }
}
