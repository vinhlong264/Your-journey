using System.Collections.Generic;
using UnityEngine;

public class itemDrop : MonoBehaviour
{
    [SerializeField] private int amountPossibleDrop; // số lượng rơi
    [SerializeField] private itemDataSO[] possibleDrop; // quản lý các item

    [SerializeField] private GameObject itemPrefabs;
    [SerializeField] private itemDataSO item;
    public void generateDrop()
    {
        if (possibleDrop.Length <= 0) return;

        for (int i = 0; i < possibleDrop.Length; i++)
        {
            int indexRandom = Random.Range(0, possibleDrop.Length);
            itemDataSO temp = possibleDrop[i];
            possibleDrop[i] = possibleDrop[indexRandom];
            possibleDrop[indexRandom] = temp;
        }

        for (int i = 0; i < amountPossibleDrop; i++)
        {
            if (Random.Range(0, 100) <= possibleDrop[i].rateDrop)
            {
                Debug.Log("Enough Lucky");
                dropItem(possibleDrop[i]);
            }
            Debug.Log("Not Enough Lucky");
        }
    }


    private void dropItem(itemDataSO _newItem)
    {
        Debug.Log("Drop item");

        if(_newItem == null) return;

        GameObject newDrop = Instantiate(itemPrefabs, transform.position, Quaternion.identity);
        Vector2 _velocity = new Vector2(Random.Range(-5, 5), Random.Range(12, 15));
        newDrop.GetComponent<itemObj>().setUpItem(_newItem, _velocity);
    }
}
