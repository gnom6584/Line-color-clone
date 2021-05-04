using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

[CustomEditor(typeof(PathGenerator))]
public class PathGeneratorEditor : Editor
{
    public override VisualElement CreateInspectorGUI()
    {
        var root = new VisualElement();

        root.Add(new IMGUIContainer(() => DrawDefaultInspector()));
        
        if (!(target is PathGenerator pathGenerator))
            return root;
        
        root.Add(new Button(() => pathGenerator.UpdatePath())
        {
            text = "Update path"
        });
        
        root.Add(new Button(() => AddBezierSegment(1))
        {
            text = "Add line"
        });
        
        for(var i = 2; i < 20; ++i)
            AddBezierButton(root, i);

        return root;
    }

    private void AddBezierButton(VisualElement root, Int32 order)
    {
        root.Add(new Button(() => AddBezierSegment(order))
        {
            text = $"Add bezier {order}"
        });
    }
    
    private void AddBezierSegment(Int32 order)
    {
        order += 1;
        
        if (!(target is PathGenerator pathGenerator))
            return;

        var transform = pathGenerator.transform;

        var segmentGo = new GameObject ($"Bezier {order - 1} segment");


        var childCount = transform.childCount;
        
        var lastSegment = childCount > 0 ? transform.GetChild(childCount - 1) : null;

        var lastMarkerPosition = lastSegment != null ? (lastSegment.childCount > 0 ? lastSegment.GetChild(lastSegment.childCount - 1).position : Vector3.zero) : Vector3.zero;

        segmentGo.transform.parent = transform;
        segmentGo.AddComponent<BezierPathSegment>();
        
        for (var i = 0; i < order; ++i)
        {
            var markerGo = new GameObject($"Point marker {i}");
            markerGo.transform.parent = segmentGo.transform;
            markerGo.transform.position = lastMarkerPosition;
        }
    }

}
