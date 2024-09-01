using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_healthBar : MonoBehaviour
{
    private Entity entity;
    private RectTransform myTransform;
    void Start()
    {
        entity = GetComponentInParent<Entity>();
        myTransform = GetComponent<RectTransform>();
        entity.onFliped += onFliped;
    }

    private void onFliped()
    {
        myTransform.Rotate(0, 180, 0);
    }    
}
