using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackHole_Skill_Controller : MonoBehaviour
{
    [SerializeField] private GameObject hotKeyPrefabs;
    [SerializeField] private List<KeyCode> ListHotKey;

    public float maxSize; // kích thước
    public float growSpeed; // tốc độ phát triển
    public bool canGrow; // kiểm tra xem có thể phát triển không
    public Vector2 DefaultLocal;


    public List<Transform> target = new List<Transform>();

    private void Start()
    {
        DefaultLocal = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        if (canGrow)
        {
            transform.localScale = Vector2.Lerp(transform.localScale , new Vector2(maxSize, maxSize), growSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Enemy>() != null)
        {

            collision.GetComponent<Enemy>().FreezeToTimer(true);

            CreateHotKey(collision);
        }
    }

    private void CreateHotKey(Collider2D collision)
    {
        if(ListHotKey.Count <= 0)
        {
            return;
        }


        GameObject newHotKey = Instantiate(hotKeyPrefabs, collision.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
        KeyCode chooseKey = ListHotKey[Random.Range(0, ListHotKey.Count)];

        ListHotKey.Remove(chooseKey);

        BlackHole_HotKey_Controller hotKetScript = newHotKey.GetComponent<BlackHole_HotKey_Controller>();
        if (hotKetScript != null)
        {
            hotKetScript.setUpKeyCode(chooseKey, collision.transform, this);
        }
    }


    public void addEnemy(Transform _enemyTransform) => target.Add(_enemyTransform);
}
