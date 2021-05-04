using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vise : MonoBehaviour
{
    public Transform LeftCollider;
    
    public Transform RightCollider;

    public Single Frequency = 1.0f;

    public static Vector3 StartLeftColliderPosition
    {
        private set;
        get;
    } = new Vector3(0.75f, 0.1f, -3.375f);

    public static Vector3 EndLeftColliderPosition
    { 
        get;
    } = new Vector3(0.75f, 0.1f, -2.875f);

    public static Vector3 StartRightColliderPosition
    {
        get;
    } = new Vector3(0.75f, 0.1f, -2.125f);

    public static Vector3 EndRightColliderPosition
    {
        get;
    } = new Vector3(0.75f, 0.1f, -2.625f);

    public void Update()
    {
        LeftCollider.localPosition = Vector3.Lerp(StartLeftColliderPosition, EndLeftColliderPosition, Mathf.Abs(Mathf.Sin(Time.time * Frequency)));
        RightCollider.localPosition = Vector3.Lerp(StartRightColliderPosition, EndRightColliderPosition, Mathf.Abs(Mathf.Sin(Time.time * Frequency)));
    }
}
