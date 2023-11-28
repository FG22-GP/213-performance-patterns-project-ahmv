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
        QuadTree = new QuadTree(0, new Rect(new Vector2(-_size.x/2, _size.y/2), _size));
    }

    private void OnDrawGizmos()
    {
        if (QuadTree == null) return;
        
        DrawRect(QuadTree.bounds);
        
        foreach (var RectBounds in QuadTree.GetAllBounds())
        {
            DrawRect(RectBounds);
        }
    }

    private void DrawRect(Rect rect)
    {
        Vector2 topLeft = rect.position;
        Vector2 bottomLeft = topLeft + new Vector2(0, -rect.height);
        Vector2 bottomRight = bottomLeft + new Vector2(rect.width, 0);
        Vector2 topRight = bottomRight + new Vector2(0, rect.height);
        
        
        Gizmos.DrawSphere(topLeft, 2f);
        Gizmos.DrawLine(bottomLeft, bottomRight);
        Gizmos.DrawLine(bottomRight, topRight);
        Gizmos.DrawLine(topRight, topLeft);
        Gizmos.DrawLine(topLeft, bottomLeft);
    }
}
