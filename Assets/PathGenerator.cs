using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathGenerator : Path.Acessor
{
    public Path Path;
    
    public void UpdatePath()
    {
        if (Path is null)
            return;
        
        var segments = GetSegments();

        if (segments.Count == 0)
            return;
        
        SetPoints(Path, segments.SelectMany(it => it.GetPoints()).Distinct().ToArray());
    }
    
    private IReadOnlyList<PathSegment> GetSegments()
    {
        var segments = new List<PathSegment>();

        foreach (Transform child in transform)
        {
            if(child.GetComponent<PathSegment>() is {} segment)
                segments.Add(segment);
        }

        return segments;
    }
}
