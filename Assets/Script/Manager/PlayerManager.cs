using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    protected static PlayerManager instance;

    public static PlayerManager Instance { get =>  instance; }




    public Player player;

    private void Awake()
    {
        if(instance != null)
        {
            DestroyImmediate(this.gameObject);
        }
        else
        {
            instance = this;
        }
                
    }

}
