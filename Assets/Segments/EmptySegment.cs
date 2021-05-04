using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class EmptySegment : PathSegment
{
    private void Update() => AttachMarkersToChildren();

    public override IReadOnlyList<Path.Point> GetPoints()
    {
        if (transform.childCount == 2)
        {
            var child0 = transform.GetChild(0).position;
            var child1 = transform.GetChild(1).position;
            
            var firstPoint = new Path.Point
            {
                Scale = Vector2.one,
                Position = child0,
            };
            var secondPoint = new Path.Point
            {
                Scale = Vector2.one,
                Position = child1,
            };
            
            return new [] {firstPoint, secondPoint};
        }
    
        throw new Exception("Round corner segment needs 2 points");
    }
}
