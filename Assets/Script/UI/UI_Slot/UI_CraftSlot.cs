using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_CraftSlot : MonoBehaviour, IPointerDownHandler
{
    private ItemEquipmentSO _equipment;
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _nameEquipment;
    [SerializeField] private UI_CraftWindow craftWindow;

    public void setUp(ItemEquipmentSO _newEquipment)
    {
        this._equipment = _newEquipment;

        if (this._equipment == null) return;

        _icon.sprite = _equipment.icon;
        _nameEquipment.text = _equipment.name;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if(_equipment == null ) return;
        if (craftWindow == null) return;

        craftWindow.setUpCraftWindow(_equipment);
    }

}
