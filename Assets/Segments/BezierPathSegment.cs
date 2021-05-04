using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[ExecuteInEditMode]
public class BezierPathSegment : PathSegment
{
    public Int32 Iterations = 25;
    
    public Color LinesColor = Color.blue;
    
    public Color CurveColor = Color.red;

    public void Update() => AttachMarkersToChildren();

    private Vector3[] GetMarkersPoint() => transform.GetChildren().Select(it => it.position).ToArray();
    
    public override IReadOnlyList<Path.Point> GetPoints()
    {
        if (Iterations == 0)
            return null;
        
        var markerPoints = GetMarkersPoint();

        var curve = new BezierCurve(markerPoints);

        var curvePoints = new Path.Point[Iterations];

        for (var i = 0; i < Iterations; ++i)
        {
            curvePoints[i].Position = curve.Evaluate(i / (Iterations - 1.0f));
            curvePoints[i].Scale = Vector2.one;
        }

        return curvePoints;
    }
    
    private void OnDrawGizmos()
    {
        var temp = Gizmos.color;
        
        var markersPoints = GetMarkersPoint();
        Gizmos.color = LinesColor;
        
        for (var i = 0; i < markersPoints.Length - 1; i++)
            Gizmos.DrawLine(markersPoints[i], markersPoints[i + 1]);
        
        Gizmos.color = CurveColor;

        var curve = GetPoints();

        for (var i = 0; i < curve.Count() - 1; i++)
            Gizmos.DrawLine(curve[i].Position, curve[i + 1].Position);

        Gizmos.color = temp;
    }
}
