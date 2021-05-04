using System;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class RoundCornerSegment : PathSegment
{
    [SerializeField] public Int32 Iterations;

    private void Update() => AttachMarkersToChildren();

    public override IReadOnlyList<Path.Point> GetPoints()
    {
        if (transform.childCount == 2)
        {
            var result = new Path.Point[Iterations];
            var child0 = transform.GetChild(0).position;
            var child1 = transform.GetChild(1).position;
            
            for (var i = 0; i < Iterations; ++i)
            {
                var t = i / (Iterations - 1.0f);
                var item = result[i];
                item.Position = Vector3.Lerp(child0, child1, t);
                item.Scale = new Vector2(Mathf.Sqrt(1.0f - (1.0f - t) * (1.0f - t)), 1.0f);
                result[i] = item;
            }

            return result;
        }
        
        throw new Exception("Round corner segment needs 2 points");
    }
}
