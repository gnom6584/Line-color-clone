using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    public Single Speed = 10.0f;
    
    public Transform Target;

    public Single Y = 4;
    
    void Start()
    {
        
    }

    void Update()
    {
        var position = transform.position;
        var targetPosition = Target.position;
        var noYPosition = new Vector3(position.x, Y, position.z);
        var noYTargetPosition = new Vector3(targetPosition.x, Y, targetPosition.z);
        transform.position = Vector3.Lerp(noYPosition, noYTargetPosition, Time.deltaTime * Speed);
    }
}
