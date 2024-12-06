using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_slotBase : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler , IPointerExitHandler
{
    protected UI ui;
    [SerializeField] protected InventoryItem item; // Item


    protected virtual void Start()
    {
        ui = GetComponentInParent<UI>();
    }

    protected Vector3 moveForMouse()
    {
        Vector3 mousePos = Input.mousePosition;
        float offSetX = 0;
        float offSetY = 0;

        if (mousePos.x > 600)
        {
            offSetX = -150f;
        }
        else
        {
            offSetX = 150f;
        }

        if (mousePos.y > 300)
        {
            offSetY = -150f;
        }
        else
        {
            offSetY = 150f;
        }

        return new Vector3(mousePos.x + offSetX , mousePos.y + offSetY , 0);
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerEnter(PointerEventData eventData)
    {
        
    }

    public virtual void OnPointerExit(PointerEventData eventData)
    {
        
    } 
}
