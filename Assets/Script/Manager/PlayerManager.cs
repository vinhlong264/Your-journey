using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance { get; private set; }  
    public Player player;

    private void Awake()
    {
        if(instance != null)
        {
            DestroyImmediate(this);
        }
        else
        {
            instance = this;
        }
        
    }
}
