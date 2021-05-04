using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class PathMeshCreator : MonoBehaviour
{
    #if UNITY_EDITOR
    [UnityEditor.CustomEditor(typeof(PathMeshCreator))]
    [UnityEditor.CanEditMultipleObjects]

    public class PathMeshCreatorEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            var root = new VisualElement();
            root.Add(new IMGUIContainer(() => DrawDefaultInspector()));
            root.Add(new Button((target as PathMeshCreator)!.ComputeMesh)
            {
                text = "Compute mesh"
            });
            return root;
        }
    }
    #endif

    [SerializeField] private Shape _shape;

    [SerializeField] private Path _path;
    
    public void ComputeMesh()
    {
        var meshFilter = GetComponent<MeshFilter>();
        
        // ReSharper disable once Unity.NoNullPropagation
        var points = _path?.Points;
        
        if (points is null || points.Count < 2 || _shape.Points.Length < 3)
        {
            meshFilter.mesh = new Mesh();
            return;
        }

        var pointsCount = points.Count;

        var shapeVertCount = _shape.Points.Length;

        var vertices = new List<Vector3>();

        var triangles = new List<Int32>();

        var mesh = new Mesh();

        Quaternion GetNormal(Int32 index) 
            => index + 1 == pointsCount 
                ? GetNormal(index - 1) 
                : Quaternion.LookRotation(points[index + 1].Position - points[index].Position);

        for (var j = 0; j < pointsCount; ++j)
            for (var i = 0; i < shapeVertCount; ++i)
                vertices.Add(GetNormal(j) * (_shape.Points[i] * points[j].Scale) + points[j].Position);

        for (var i = 0; i < pointsCount - 1; ++i)
        {
            var offset = i * shapeVertCount;
            for (var j = 0; j < shapeVertCount; ++j)
            {
                triangles.Add(offset + j + shapeVertCount);
                triangles.Add(offset + j);
                triangles.Add(offset + (j + 1) % shapeVertCount);

                triangles.Add(offset + (j + 1) % shapeVertCount);
                triangles.Add(offset + shapeVertCount + (j + 1) % shapeVertCount);
                triangles.Add(offset + j + shapeVertCount);
            }
        }
     
        
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateNormals();
        
        meshFilter.mesh = mesh;
    }
}
