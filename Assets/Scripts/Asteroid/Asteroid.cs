using UnityEngine;
using UnityEngine.Events;
using System;

public class AstroidEvent : UnityEvent<Asteroid> {};

public class Asteroid : MonoBehaviour, IDamageable
{

    #region Enums

    public enum AsteroidType
    {
        Large,
        Medium,
        Small
    }

    #endregion

    #region Events
    
    [NonSerialized] public AstroidEvent onDestroyed = new AstroidEvent();
    
    #endregion

    #region Unity Exposed Variables
    
    [SerializeField] private Transform artwork;
    [SerializeField] private Range velocityRange = new Range { min = 0.5f, max = 1.5f };
    
    #endregion

    #region Private Variables
    
    private new Collider2D collider;
    private new Rigidbody2D rigidbody2D;
    private Vector2 velocity;

    #endregion

    #region Properties

    public AsteroidType asteroidType { get; protected set; }

    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        collider = GetComponent<Collider2D>();
    }

    void OnEnable()
    {
        rigidbody2D.velocity = velocity;
    }

    void Update()
    {
        transform.localRotation *= Quaternion.Euler(velocity);
        transform.position = EuclideanTorus.current.Wrap(transform.position, collider.bounds.extents.x);
    }


    void OnCollisionEnter2D(Collision2D collision)
    {

        rigidbody2D.velocity = Vector3.Reflect(rigidbody2D.velocity, collision.contacts[0].normal) * 1.25f;
    }

    #endregion

    #region Public Methods

    public void SetType(AsteroidType type)
    {
        asteroidType = type;
        velocity = Quaternion.Euler(0, 0, UnityEngine.Random.Range(-180f, 180f)) * Vector3.up * velocityRange.Random();
        switch(type)
        {
            case AsteroidType.Large:
                transform.localScale = Vector3.one * 2;
                break;
            case AsteroidType.Medium:
                transform.localScale = Vector3.one * 1;
                break;
            case AsteroidType.Small:
                transform.localScale = Vector3.one * 0.5f;
                velocity *= 1.5f;
                break;
        }
    }

    public void Damage()
    {
        onDestroyed.Invoke(this);    
    }

    #endregion

}