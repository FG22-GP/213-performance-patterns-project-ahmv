using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

[RequireComponent(typeof(PooledObject))]
public class Projectile : MonoBehaviour
{
    private PooledObject pooledObject;
    private float _totalTime;
    void Start()
    {
        pooledObject = GetComponent<PooledObject>();
        FakeInitializeProjectile();
    }

    /// <summary>
    /// Setting up complex Prefabs containing Models, Sprites, Materials etc.
    /// Is Expensive. This Call simulates a more complex Object.
    /// Do not remove this Method invocation from Start.
    /// Instead, try to avoid `Start` being invoked in the first place. 
    /// </summary>
    void FakeInitializeProjectile()
    {
        print("Sleep");
        Thread.Sleep(100);
    }
    
    // Update is called once per frame
    void Update()
    {
        this._totalTime += Time.deltaTime;
        this.transform.Translate(Vector3.up * Time.deltaTime);
        if (this._totalTime > 10f)
        {
            if (pooledObject == null) pooledObject = GetComponent<PooledObject>();
            pooledObject.ReturnToPool();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("On Collision!");
        if (pooledObject == null) pooledObject = GetComponent<PooledObject>();
        pooledObject.ReturnToPool();
    }
}
