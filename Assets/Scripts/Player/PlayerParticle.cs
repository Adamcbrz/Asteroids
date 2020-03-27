using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerParticleEvent : UnityEvent<PlayerParticle> { }

public class PlayerParticle : MonoBehaviour
{
    #region Public Events
    
    [NonSerialized] public PlayerParticleEvent onDisposed = new PlayerParticleEvent();

    #endregion

    #region Unity Exposed Variables

    [SerializeField] public float lifetime = 5;

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
