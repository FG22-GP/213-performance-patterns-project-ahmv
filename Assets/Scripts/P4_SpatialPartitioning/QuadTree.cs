using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class QuadTree
{
    private const int MaxObjectsPerNode = 10;
    private const int MaxLevels = 5;

    private int level;
    private List<GameObject> objects;
    public Rect bounds;
    public QuadTree[] children;

    public QuadTree(int level, Rect bounds)
    {
        this.level = level;
        this.bounds = bounds;
        objects = new List<GameObject>();
        children = new QuadTree[4];
    }

    public void Clear()
    {
        objects.Clear();

        for (int i = 0; i < children.Length; i++)
        {
            if (children[i] != null)
            {
                children[i].Clear();
                children[i] = null;
            }
        }
    }

    private void Split()
    {
        float subWidth = bounds.width / 2f;
        float subHeight = bounds.height / 2f;
        float x = bounds.x;
        float y = bounds.y;

        children[0] = new QuadTree(level + 1, new Rect(x - subWidth, y - subHeight, subWidth, subHeight));
        children[1] = new QuadTree(level + 1, new Rect(x + subWidth, y - subHeight, subWidth, subHeight));
        children[2] = new QuadTree(level + 1, new Rect(x - subWidth, y + subHeight, subWidth, subHeight));
        children[3] = new QuadTree(level + 1, new Rect(x + subWidth, y + subHeight, subWidth, subHeight));

    }

    private int GetIndex(GameObject obj)
    {
        int index = -1;
        float verticalMidpoint = bounds.y + bounds.height / 2f;

        // Object can fit completely within the top quadrants
        bool topQuadrant = obj.transform.position.y > verticalMidpoint;

        // Object can fit completely within the bottom quadrants
        bool bottomQuadrant = obj.transform.position.y < verticalMidpoint;

        // Object can fit completely within the left quadrants
        if (obj.transform.position.x < bounds.x && obj.transform.position.x - obj.transform.localScale.x / 2f < bounds.x)
        {
            if (topQuadrant)
            {
                index = 1;
            }
            else if (bottomQuadrant)
            {
                index = 2;
            }
        }
        // Object can fit completely within the right quadrants
        else if (obj.transform.position.x > bounds.x && obj.transform.position.x + obj.transform.localScale.x / 2f > bounds.x)
        {
            if (topQuadrant)
            {
                index = 0;
            }
            else if (bottomQuadrant)
            {
                index = 3;
            }
        }

        return index;
    }

    public void Insert(GameObject obj)
    {
        if (children[0] != null)
        {
            int index = GetIndex(obj);

            if (index != -1)
            {
                children[index].Insert(obj);
                return;
            }
        }

        objects.Add(obj);

        if (objects.Count > MaxObjectsPerNode && level < MaxLevels)
        {
            if (children[0] == null)
            {
                Split();
            }

            int i = 0;
            while (i < objects.Count)
            {
                int index = GetIndex(objects[i]);
                if (index != -1)
                {
                    children[index].Insert(objects[i]);
                    objects.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }

    public List<GameObject> Retrieve(List<GameObject> returnObjects, GameObject obj)
    {
        int index = GetIndex(obj);

        if (index != -1 && children[0] != null)
        {
            children[index].Retrieve(returnObjects, obj);
        }

        returnObjects.AddRange(objects);

        return returnObjects;
    }

    public List<Rect> GetAllBounds()
    {
        List<Rect> allBounds = new List<Rect>();
        
        allBounds.Add(bounds);
        
        foreach (var child in children)
        {
            if(child == null)continue;
            
            allBounds.AddRange(child.GetAllBounds());
            
            Debug.Log(allBounds.Count);
        }
        
        return allBounds;
    }
}