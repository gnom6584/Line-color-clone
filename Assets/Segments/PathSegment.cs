using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class PathSegment : MonoBehaviour
{
    public Color MarkersColor = Color.green;

    public abstract IReadOnlyList<Path.Point> GetPoints();
    
    protected void AttachMarkersToChildren()
    {
        foreach (Transform child in transform)
        {
            var existMarker = child.GetComponent<PointMarker>();
            if(!existMarker)
                existMarker = child.gameObject.AddComponent<PointMarker>();
            existMarker.Color = MarkersColor;
        }
    }
}
