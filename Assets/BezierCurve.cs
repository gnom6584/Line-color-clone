using System;
using System.Collections.Generic;
using UnityEngine;

public interface IParametricCurve<out T>
{
    T Evaluate(Single t);
}

public sealed class BezierCurve : IParametricCurve<Vector3>
{
    public readonly IReadOnlyList<Vector3> Points;

    public BezierCurve(IReadOnlyList<Vector3> points) => Points = points;

    public static Int32 NewtonBinomial(Int32 level, Int32 index)
    {
        if (level == index || index == 0)
            return 1;
        return NewtonBinomial(level - 1, index - 1) * level / index;
    }
    
    public Vector3 Evaluate(Single t)
    {
        var sum = default(Vector3);

        var n = Points.Count - 1;

        var oneMinusT = 1.0f - t;

        for (var i = 0; i < n + 1; ++i)
        {
            var binomial = NewtonBinomial(n, i);
            sum += Points[i] * Mathf.Pow(oneMinusT, n - i) * Mathf.Pow(t, i) * binomial;
        }

        return sum;
    }
}

public sealed class BezierCurve3 : IParametricCurve<Vector3>
{
    public readonly Vector3 P0;
    
    public readonly Vector3 P1;
    
    public readonly Vector3 P2;
    
    public readonly Vector3 P3;

    public BezierCurve3(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
        => (P0, P1, P2, P3) = (p0, p1, p2, p3);
    
    public Vector3 Evaluate(Single t)
    {
        var oneMinusT = 1.0f - t;
        
        return P0 * oneMinusT * oneMinusT * oneMinusT + 3.0f * oneMinusT * oneMinusT * t * P1 + 3.0f * oneMinusT * t * t * P2 + P3 * t * t * t;
    }
}

public sealed class BezierCurve2 : IParametricCurve<Vector3>
{
    public readonly Vector3 P0;
    
    public readonly Vector3 P1;
    
    public readonly Vector3 P2;
    

    public BezierCurve2(Vector3 p0, Vector3 p1, Vector3 p2)
        => (P0, P1, P2) = (p0, p1, p2);
    
    public Vector3 Evaluate(Single t)
    {
        var oneMinusT = 1.0f - t;
        
        return P0 * oneMinusT * oneMinusT + P1 * 2.0f * oneMinusT * t + P2 * t * t;
    }
}