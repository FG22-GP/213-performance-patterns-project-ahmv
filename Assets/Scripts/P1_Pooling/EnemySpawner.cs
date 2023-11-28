using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private ObjectPool enemyPool;
    
    public Enemy EnemyPrefab;
    private const float _totalCooldown = 2f;
    private float _currentCooldown;

    private void Start()
    {
        if (enemyPool == null)
        {
            print("Enemy Pool not in place");
            Application.Quit();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        enemyPool.FutureObjectCount = Mathf.CeilToInt((Time.timeSinceLevelLoad + 5)/ 7);
        
        this._currentCooldown -= Time.deltaTime;
        if (this._currentCooldown <= 0f)
        {
            this._currentCooldown += _totalCooldown;
            SpawnEnemies();
        }
    }


    void SpawnEnemies()
    {
        var maxAmount = Mathf.CeilToInt(Time.timeSinceLevelLoad / 7);
        int amount = Random.Range(maxAmount, maxAmount + 3);
        for (var i = 0; i < amount; i++)
        {
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        float randomPositionX = Random.Range(-6f, 6f);
        float randomPositionY = Random.Range(-6f, 6f);
        
        PooledObject enemy = enemyPool.RequestPooledObject();
        Transform enemyTransform = enemy.transform;
        enemyTransform.position = new Vector2(randomPositionX, randomPositionY);
        enemyTransform.rotation = Quaternion.identity;
    }
}
