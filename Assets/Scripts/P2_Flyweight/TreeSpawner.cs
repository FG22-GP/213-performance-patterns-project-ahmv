using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;
using Random = UnityEngine.Random;

public class TreeSpawner : MonoBehaviour
{
    public Tree TreePrefab;
    private float _currentCooldown;
    private const float TotalCooldown = 0.2f;
    [SerializeField] private FlyWeight _flyWeight;
    
    private void Start()
    {
        _flyWeight.Setup();
    }
    
    private void FixedUpdate()
    {
        _currentCooldown -= Time.deltaTime;
        if (_currentCooldown <= 0f)
        {
            _currentCooldown += TotalCooldown;
            SpawnTree();
        }
    }

    private void SpawnTree()
    {
        var randomPositionX = Random.Range(-6f, 6f);
        var randomPositionY = Random.Range(-6f, 6f);
        Tree spawnedTree = Instantiate(TreePrefab, new Vector2(randomPositionX, randomPositionY), Quaternion.identity);
        spawnedTree._treeColors.flyWeight = _flyWeight;
    }
}
