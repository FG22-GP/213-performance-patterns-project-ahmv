using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledObject : MonoBehaviour
{
    private ObjectPool poolParent;
    public ObjectPool PoolParent { get => poolParent; set => poolParent = value; }

    public void ReturnToPool()
    {
        PoolParent.ReturnToPool(this);
    }
}
