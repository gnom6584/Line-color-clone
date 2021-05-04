using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public sealed class PathsTraversal : MonoBehaviour
{
    [Serializable]
    public class UnityEventVector3 : UnityEvent<Vector3> {}
    
    public Path[] Paths;
    
    private Vector3[] _mergedPath;

    private Single _pathLength;
    
    public (Vector3 position, Vector3 direction) Evaluate(Single t) => EvaluateEquidistant(t);
    
    // TODO: O(N) complexity, need optimization
    private (Vector3 position, Vector3 direction) EvaluateEquidistant(Single t)
    {
        t = Mathf.Clamp(t, 0.0f, 1.0f);
        var targetLength = t * _pathLength;

        var currentLength = 0.0f;
        
        var i = 0;
        for (; i < _mergedPath.Length; i++)
        {
            currentLength += Vector3.Distance(_mergedPath[i], _mergedPath[i + 1]);
            if(currentLength >= targetLength)
                break;
        }

        var diff = (_mergedPath[i + 1] - _mergedPath[i]);
        var direction = diff.normalized;
        
        return (_mergedPath[i] + direction * (diff.magnitude - currentLength + targetLength), direction);
    }
    
    private void Awake()
    {
        _mergedPath = Paths.SelectMany(path => path.Points.Select(point => point.Position)).Distinct().ToArray();
        for (var i = 0; i < _mergedPath.Length; i++)
        {
            if (i + 1 == _mergedPath.Length)
                break;
            _pathLength += Vector3.Distance(_mergedPath[i], _mergedPath[i + 1]);
        }
    }
}
