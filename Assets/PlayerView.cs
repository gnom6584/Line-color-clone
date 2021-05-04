using System;
using UnityEngine;
using UnityEngine.Events;

public class PlayerView : MonoBehaviour
{
    public UnityEvent OnCollide;
    
    public TrailRenderer Trail;

    public ParticleSystem MoveParticles;
    
    public ParticleSystem DestroyParticles;

    public MeshRenderer MeshRenderer;
    
    public void OnStartPush()
    {
        MoveParticles.enableEmission = true;
    }
    
    public void OnStopPush()
    {
        MoveParticles.enableEmission = false;
    }

    public void OnPositionChange(Vector3 position) => transform.position = position + Vector3.Cross(Vector3.Cross(transform.forward, Vector3.up), transform.forward).normalized * (transform.localScale.y / 2.0f);

    public void OnDirectionChange(Vector3 direction) => transform.forward = direction;

    public void OnReset()
    {
        Trail.Clear();
        MeshRenderer.enabled = true;
    }
    
    public void OnFail()
    {
        MoveParticles.enableEmission = false;
        DestroyParticles.Play();
        MeshRenderer.enabled = false;
    }

    private void OnCollisionEnter(Collision other) => OnCollide?.Invoke();
}
