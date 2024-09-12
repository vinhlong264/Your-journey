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
        Inventory.Instance.addItem(item);
        Destroy(gameObject);
    }
}
