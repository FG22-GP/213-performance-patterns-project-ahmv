using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class QuadTreeReferenceHolder : MonoBehaviour
{
    public QuadTree QuadTree;
    [SerializeField] private Vector2 _size;
    private void Awake()
    {
        QuadTree = new QuadTree(0, new Rect(Vector2.zero, _size));
    }

    private void OnDrawGizmos()
    {
        if (QuadTree == null) return;
        
        Gizmos.DrawWireCube(QuadTree.bounds.center, QuadTree.bounds.size);
        
        foreach (var RectBounds in QuadTree.GetAllBounds())
        {
            Gizmos.DrawWireCube(RectBounds.center, RectBounds.size);    
            Gizmos.DrawSphere(RectBounds.position, 5f);
        }
        
        
    }
}
