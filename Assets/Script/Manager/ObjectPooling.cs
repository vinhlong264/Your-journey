using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : Singleton<ObjectPooling>
{
    private Stack<GameObject> pooling = new Stack<GameObject>();
    protected override void Awake()
    {
        base.Awake();
    }
}
