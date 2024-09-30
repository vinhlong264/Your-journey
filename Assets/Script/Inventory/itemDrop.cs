using UnityEngine;

public class itemDrop : MonoBehaviour
{
    [SerializeField] private int amountPossibleDrop; // số lượng rơi
    [SerializeField] private itemDataSO[] possibleDrop; // quản lý các item
    //[SerializeField] private List<itemDataSO> possibleDropList = new List<itemDataSO>(); // lưu item

    [SerializeField] private GameObject itemPrefabs;
    [SerializeField] private itemDataSO item;
    public void generateDrop()
    {
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
                dropItem(possibleDrop[i]);
            }
        }
    }


    private void dropItem(itemDataSO _newItem)
    {
        GameObject newDrop = Instantiate(itemPrefabs, transform.position, Quaternion.identity);
        Vector2 _velocity = new Vector2(Random.Range(-5, 5), Random.Range(12, 15));
        newDrop.GetComponent<itemObj>().setUpItem(_newItem, _velocity);
    }
}
