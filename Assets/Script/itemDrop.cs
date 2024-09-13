using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemDrop : MonoBehaviour
{
    [SerializeField] private int possibleItemDrop; // số lượng rơi
    [SerializeField] private itemDataSO[] possibleDrop; // quản lý các item
    [SerializeField] private List<itemDataSO> possibleDropList; // lưu item

    [SerializeField] private GameObject itemPrefabs;
    [SerializeField] private itemDataSO item;

    private void Start()
    {
        possibleDropList = new List<itemDataSO>();
    }

    public void generateDrop()
    {
        for (int i = 0; i < possibleDrop.Length - 1; i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].rateDrop) // so sánh tỉ lệ giữa các item trong mảng
            {
                possibleDropList.Add(possibleDrop[i]);
            }
        }

        for (int i = 0; i < possibleItemDrop; i++)
        {
            itemDataSO _itemDropRandom = possibleDropList[Random.Range(0, possibleDropList.Count - 1)];

            possibleDropList.Remove(_itemDropRandom);
            dropItem(_itemDropRandom);
        }
    }


    private void dropItem(itemDataSO _newItem)
    {
        GameObject newDrop = Instantiate(itemPrefabs, transform.position, Quaternion.identity);

        Vector2 _velocity = new Vector2(Random.Range(-5, 5), Random.Range(12, 15));

        newDrop.GetComponent<itemObj>().setUpItem(_newItem, _velocity);
    }
}
