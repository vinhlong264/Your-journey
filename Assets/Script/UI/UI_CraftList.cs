using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private List<ItemEquipmentSO> equipmentList = new List<ItemEquipmentSO>(); // List chứa các equipment
    [Space]
    [SerializeField] private Transform ui_SlotCratfParent;
    [SerializeField] private UI_CraftSlot[] ui_CraftSlot;

    void Start()
    {
        ui_CraftSlot = ui_SlotCratfParent.GetComponentsInChildren<UI_CraftSlot>();


        SetUpListCraft();
    }

    private void SetUpListCraft()
    {
        if (ui_CraftSlot.Length <= 0 || equipmentList.Count == 0) return;

        for(int i = 0; i < equipmentList.Count; i++)
        {
            ui_CraftSlot[i].setUp(equipmentList[i]);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Click");
        SetUpListCraft();
    }
}
