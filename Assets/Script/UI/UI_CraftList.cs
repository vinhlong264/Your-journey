using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour , IPointerClickHandler
{
    [SerializeField] private Transform craftSlotParent;
    [SerializeField] private List<UI_CraftSlot> craftsSlot;
    [SerializeField] private GameObject craftSlotObj;
    [SerializeField] private List<ItemEquipmentSO> craftEquipment = new List<ItemEquipmentSO>();
    [SerializeField] private List<itemDataSO> itemMaterial;
    void Start()
    {
        AssignCraftSlot();
    }

    private void AssignCraftSlot()
    {
        for (int i = craftSlotParent.childCount - 1; i >= 0; i--)
        {
            craftsSlot.Add(craftSlotParent.GetChild(i).GetComponent<UI_CraftSlot>());
        }
    }

    public void setUpCraftList()
    {
        for(int i = 0; i < craftsSlot.Count; i++)
        {
            Destroy(craftsSlot[i].gameObject);
        }

        craftsSlot = new List<UI_CraftSlot>();

        for(int i = 0; i < craftEquipment.Count; i++)
        {
            GameObject newGameObject = Instantiate(craftSlotObj , craftSlotParent);
            newGameObject.GetComponent<UI_CraftSlot>().setUpCraftSlot(craftEquipment[i]);

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        setUpCraftList();
    }
}