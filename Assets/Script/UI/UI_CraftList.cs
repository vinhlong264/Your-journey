using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CraftList : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Transform craftSlotParent;
    [SerializeField] private List<UI_CraftSlot> craftsSlot;
    [SerializeField] private GameObject craftSlotObj;
    [SerializeField] private List<ItemEquipmentSO> craftEquipment = new List<ItemEquipmentSO>();
    [SerializeField] private List<itemDataSO> itemMaterial;
    void Start()
    {
        transform.parent.GetChild(0).GetComponent<UI_CraftList>().setUpCraftList();
        setDefaultEquipmentCraft();
    }


    public void setUpCraftList()
    {
        for (int i = 0; i < craftSlotParent.childCount; i++)
        {
            Destroy(craftSlotParent.GetChild(i).gameObject);
        }

        craftsSlot = new List<UI_CraftSlot>();

        for (int i = 0; i < craftEquipment.Count; i++)
        {
            GameObject newGameObject = Instantiate(craftSlotObj, craftSlotParent);
            newGameObject.GetComponent<UI_CraftSlot>().setUpCraftSlot(craftEquipment[i]);

        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        setUpCraftList();
    }

    private void setDefaultEquipmentCraft()
    {
        if (craftEquipment[0] != null)
        {
            GetComponentInParent<UI>().uiCanCraftWindow.setUpCraftWindow(craftEquipment[0]);
        }
    }
}
