using System.Collections.Generic;
using UnityEngine;

public static class TransformExt
{
    public static void RemoveChildren(this Transform transform)
    {
        foreach (Transform child in transform) 
            Object.Destroy(child.gameObject);
    }

    public static IReadOnlyList<Transform> GetChildren(this Transform transform)
    {
        var children = new Transform[transform.childCount];
        
        var i = 0;
        foreach (Transform child in transform)
        {
            children[i] = child;
            ++i;
        }

        return children;
    }
}
