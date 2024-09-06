using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemObj : MonoBehaviour
{
    [SerializeField] private itemDataSO item;
    private SpriteRenderer sr;

    private void OnValidate()
    {
        GetComponent<SpriteRenderer>().sprite = item.icon;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Player>() != null)
        {
            Inventory.Instance.addItem(item);
            Destroy(gameObject);
        }
    }
}
