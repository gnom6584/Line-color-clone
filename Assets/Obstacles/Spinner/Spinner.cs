using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private Single speedRotation = 200.0f;
    
    public void Update()
    {
        transform.Rotate(0.0f, Time.deltaTime * speedRotation, 0.0f);
    }
}
