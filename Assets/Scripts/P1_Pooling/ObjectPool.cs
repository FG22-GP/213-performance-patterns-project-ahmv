using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEngine;
using Task = System.Threading.Tasks.Task;

public class ObjectPool : MonoBehaviour
{
    [SerializeField] private uint initPoolSize;
    [SerializeField] private PooledObject objectToPool;

    private int ObjectCount => objects.Count;
    [HideInInspector] public int FutureObjectCount = 0;
    
    private Stack<PooledObject> objects;

    private void Awake()
    {
        SetupPool();
    }

    private async void SetupPool()
    {
        objects = new Stack<PooledObject>();

        for (int i = 0; i < initPoolSize; i++)
        {
            MakeInstance();
        }

        await Task.Yield();

        foreach (PooledObject pooledObject in objects)
        {
            pooledObject.gameObject.SetActive(false);
        }
    }

    private void MakeInstance()
    {
        PooledObject instance = Instantiate(objectToPool);
        instance.PoolParent = this;
        objects.Push(instance);
    }

    private void Update()
    {
        if (ObjectCount < FutureObjectCount)
        {
            MakeInstance();
        }
    }

    public PooledObject RequestPooledObject()
    {
        if (objects.Count <= 0)
        {
            PooledObject newInstance = Instantiate(objectToPool);
            newInstance.PoolParent = this;
            print("New Instance");
            return newInstance;
        }
        
        PooledObject instance = objects.Pop();
        instance.gameObject.SetActive(true);
        return instance;
    }

    public void ReturnToPool(PooledObject pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        objects.Push(pooledObject);
    }
}
