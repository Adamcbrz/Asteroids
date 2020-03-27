using System;
using UnityEngine;
using UnityEngine.Events;

public class BulletEvent : UnityEvent<Bullet> { }

public class Bullet : MonoBehaviour
{
    #region Unity Exposed Variables

    [SerializeField] private float bulletRate = 100;
    [SerializeField] private float lifetime = 5;

    #endregion

    #region Public Variables

    [NonSerialized] public BulletEvent onDispose = new BulletEvent();

    #endregion

    #region Private Variables

    private Rigidbody2D _rigidbody2D;

    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        Invoke("Dispose", lifetime);
    }

    void OnDisable()
    {
        _rigidbody2D.velocity = Vector2.zero;
    }

    void FixedUpdate()
    {
        Vector3 pos = (Vector3)_rigidbody2D.position + transform.up * bulletRate * Time.fixedDeltaTime;
        _rigidbody2D.position = EuclideanTorus.current.Wrap(pos);
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent(typeof(IDamageable)) as IDamageable;
        if(damageable != null)
        {
            damageable.Damage();
            Dispose();
        }
    }

    #endregion

    #region Private Methods

    void Dispose()
    {
        onDispose.Invoke(this);
        CancelInvoke();
    }

    #endregion
}
