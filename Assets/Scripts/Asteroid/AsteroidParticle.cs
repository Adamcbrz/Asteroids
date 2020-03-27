using System;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidParticleEvent : UnityEvent<AsteroidParticle> { }
public class AsteroidParticle : MonoBehaviour
{
    [NonSerialized] public AsteroidParticleEvent onDisposed = new AsteroidParticleEvent();
    [SerializeField] private float lifetime = 5;

    void OnEnable()
    {
        Invoke("Dispose", lifetime);
    }

    void OnDisable()
    {
        CancelInvoke("Dispose");
    }

    void Dispose()
    {
        onDisposed.Invoke(this);
    }
}
