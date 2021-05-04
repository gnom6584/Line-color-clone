using System;
using UnityEngine;

public class PointMarker : MonoBehaviour
{
    public Single Radius = 0.1f;

    public Color Color = Color.green;
    
    private void OnDrawGizmos()
    {
        var temp = Gizmos.color;

        Gizmos.color = Color;
        
        Gizmos.DrawSphere(transform.position, Radius);
        
        Gizmos.color = temp;
    }
}
