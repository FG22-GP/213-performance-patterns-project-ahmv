using System;
using UnityEngine;

[Serializable]
public class TreeSeasonColors
{
    public FlyWeight flyWeight;
    private ColorInfo[] Colors => flyWeight.TreeColors.colors;
    
    /// <summary>
    /// This returns the current color. The value changes every time
    /// `MoveNext` is invoked.
    /// </summary>
    public Color CurrentColor
    {
        get
        {
            var colorInfo = Colors[_index];
            return new Color(colorInfo.r / 255.0f, colorInfo.g / 255.0f, colorInfo.b / 255.0f, 1f);
        }
    }

    /// <summary>
    /// This makes the Tree move on to the next color
    /// </summary>
    public void MoveNext()
    {
        _index += 10;
        _index %= Colors.Length;
    }

    private int _index;
}