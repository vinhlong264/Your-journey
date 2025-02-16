using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static GameManager instance;

    public Player player;
    public PlayerStats playerStats;
    public PlayerLevel playerLevel;

    private Dictionary<GameObject,List<GameObject>> poolDic = new Dictionary<GameObject,List<GameObject>>();

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public GameObject GetObjFromPool(GameObject keyObj)
    {
        List<GameObject> dumpListObj = new List<GameObject>();

        if (poolDic.ContainsKey(keyObj))
        {
            Debug.LogWarning("Pool tồn tại key");
            dumpListObj = poolDic[keyObj];
        }
        else
        {
            Debug.LogWarning("Pool không tồn tại key");
            poolDic.Add(keyObj, dumpListObj);
        }

        foreach (GameObject g in dumpListObj)
        {
            if (g.activeSelf)
            {
                continue;
            }
            g.SetActive(true);
            return g;
        }

        GameObject newObj = Instantiate(keyObj);
        dumpListObj.Add(newObj);
        newObj.SetActive(true);

        return newObj;

    }


    public void ReturnPool(GameObject objReturn)
    {
        if (poolDic.ContainsKey(objReturn))
        {
            poolDic[objReturn].Add(objReturn);
        }
        else
        {
            poolDic.Add(objReturn, new List<GameObject>());
            poolDic[objReturn].Add(objReturn);
        }
    }
}
