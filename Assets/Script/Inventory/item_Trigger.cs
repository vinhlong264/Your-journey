using UnityEngine;

public class item_Trigger : MonoBehaviour
{
    private itemObj myItem;
    void Start()
    {
        myItem = GetComponentInParent<itemObj>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>() != null)
        {
            myItem.itemPickUp();
        }
    }
}
