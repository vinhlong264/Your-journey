using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static GameManager instance;

    public Player player;
    public PlayerStats playerStats;
    public PlayerLevel playerLevel;

    private Dictionary<GameObject,GameObject> poolDic = new Dictionary<GameObject,GameObject>();
    private GameObject tmp;

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public GameObject GetObjFromPool(GameObject baseObj)
    {
        if (poolDic.ContainsKey(baseObj))
        {
            tmp = poolDic[baseObj];
            tmp.SetActive(true);
            return tmp;
        }

        tmp = Instantiate(baseObj);
        poolDic.Add(baseObj, tmp);
        return tmp;
    }
}
