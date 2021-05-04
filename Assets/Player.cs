using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    [Serializable]
    public class UnityEventVector3 : UnityEvent<Vector3> {}
    
    [Serializable]
    public class UnityEventSingle : UnityEvent<Single> {}

    public const String FireAxis = "Fire1";
    
    public PathsTraversal _PathsTraversal;
    
    public Single Speed = 0.01f;

    public Single Friction = 10.0f;
    
    public Boolean IsFailed { private set; get; }

    public Boolean IsCompleted => _travelValue >= 1.0f;

    public UnityEvent OnFail;
    
    public UnityEvent OnComplete;

    public UnityEvent OnReset;
    
    public UnityEventVector3 OnPositionChange;
    
    public UnityEventVector3 OnDirectionChange;

    public UnityEventSingle OnProgressChange;

    public UnityEvent OnStartPush;
    
    public UnityEvent OnStopPush;
    
    private Single _travelValue;

    private Single _velocity;
    
    public void Reset()
    {
        UpdateTravelValue(0);
        OnReset?.Invoke();
        IsFailed = false;
    }

    private void Update()
    {
        if (IsCompleted)
            return;
        
        if (IsFailed)
        {
            if (Input.GetButtonDown(FireAxis))
            {
                OnStartPush?.Invoke();
                Reset();
            }

            return;
        }

        if(Input.GetButtonDown(FireAxis))
            OnStartPush?.Invoke();
        
        if(Input.GetButtonUp(FireAxis))
            OnStopPush?.Invoke();

        _velocity = Mathf.Lerp(_velocity, Input.GetButton(FireAxis) ? Speed : 0.0f, Time.deltaTime * Friction);
            
        UpdateTravelValue(_travelValue + Time.deltaTime * _velocity);

        if(IsCompleted) 
            OnComplete?.Invoke();
    }
    
    private void UpdateTravelValue(Single travelValue)
    {
        if(_travelValue == travelValue)
            return;
        
        _travelValue = travelValue;
        
        var (position, direction) = _PathsTraversal.Evaluate(travelValue);

        OnProgressChange?.Invoke(travelValue);
        OnDirectionChange?.Invoke(direction);
        OnPositionChange?.Invoke(position);
    }

    public void OnCollide()
    {
        if (!IsFailed)
        {
            OnFail?.Invoke();
            IsFailed = true;
        }
    }
}
