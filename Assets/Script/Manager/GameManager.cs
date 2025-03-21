using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private static GameManager instance;

    [Header("References Infor")]
    [SerializeField] private Player player;
    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private PlayerLevel playerLevel;

    private Dictionary<GameObject,List<GameObject>> poolDic = new Dictionary<GameObject,List<GameObject>>();

    #region Get
    public Player Player { get => player; }
    public PlayerStats PlayerStats { get => playerStats; }
    public PlayerLevel PlayerLevel { get => playerLevel; }
    #endregion

    protected override void Awake()
    {
        MakeSingleton(false);
    }

    public GameObject GetObjFromPool(GameObject keyObj)
    {
        List<GameObject> dumpListObj = new List<GameObject>();

        if (poolDic.ContainsKey(keyObj))
        {
            dumpListObj = poolDic[keyObj];
        }
        else
        {
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
