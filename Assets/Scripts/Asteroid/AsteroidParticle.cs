using System;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidParticleEvent : UnityEvent<AsteroidParticle> { }
public class AsteroidParticle : MonoBehaviour
{
    #region Events

    [NonSerialized] public AsteroidParticleEvent onDisposed = new AsteroidParticleEvent();

    #endregion

    #region Unity Exposed Variable 

    [SerializeField] private float lifetime = 5;

    #endregion

    #region Unity Lifecycle

    void OnEnable()
    {
        Invoke("Dispose", lifetime);
    }

    void OnDisable()
    {
        CancelInvoke("Dispose");
    }

    #endregion

    #region Private Methods

    void Dispose()
    {
        onDisposed.Invoke(this);
    }

    #endregion
}
