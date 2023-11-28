using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyWeight : MonoBehaviour
{
    public ColorHolder TreeColors { get; private set; }
    
    public void Setup()
    {
        var fileContents = Resources.Load<TextAsset>("treeColors").text;
        TreeColors = JsonUtility.FromJson<ColorHolder>(fileContents);
    }
}

public class ColorHolder
{
    public ColorInfo[] colors;

}