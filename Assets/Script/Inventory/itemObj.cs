using UnityEngine;

public class itemObj : MonoBehaviour
{
    [SerializeField] private itemDataSO item;
    private Rigidbody2D rb;
    private SpriteRenderer sr;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnValidate()
    {
        if (item == null) return;

        itemVisual();
    }


    public void setUpItem(itemDataSO _item, Vector2 _velicity)
    {
        item = _item;
        rb.velocity = _velicity;

        itemVisual();
    }
    private void itemVisual()
    {
        GetComponent<SpriteRenderer>().sprite = item.icon;
        gameObject.name = item.name;
    }

    public void itemPickUp()
    {
        /*if (!Inventory.Instance.canAddItem() && item.ItemType == ItemType.Equipment) return;*/ // điều kiện để giới hạn số lượng có thể thỏa mãn số lượng trong Inventory


        InventorySystem.Instance.addItem(item);
        Destroy(gameObject);
    }
}
