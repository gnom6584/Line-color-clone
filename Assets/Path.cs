using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Path", menuName = "Path")]
public sealed class Path : ScriptableObject
{
    [Serializable]
    public struct Point
    {
        public Vector3 Position;

        public Vector2 Scale;

    }
    
    public class Acessor : MonoBehaviour
    {
        protected void SetPoints(Path path, Point[] points)
        {
            #if UNITY_EDITOR
            var so = new UnityEditor.SerializedObject(this);
            path._points = points;
            so.ApplyModifiedProperties();
            #endif
        } 

    }
    
    [SerializeField] private Point[] _points;       
    public IReadOnlyList<Point> Points => _points;
}
