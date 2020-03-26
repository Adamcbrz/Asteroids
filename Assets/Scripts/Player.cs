using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[System.Serializable]
public class PlayerAttack
{
    private IPlayerAttackingHandler handler;
    private float fireTime = 0;

    [SerializeField] private GameObject prefabBullet;
    [SerializeField] private int intialBulletAmount = 10;
    [SerializeField] private Transform fireLocation;
    [SerializeField] private float fireDelay = 2;
    private ItemPool<Bullet> bulletPool;
    
    public PlayerAttack()
    {
        bulletPool = new ItemPool<Bullet>(prefabBullet, intialBulletAmount);
    }

    public void SetAttackHandler(IPlayerAttackingHandler handler)
    {
        this.handler = handler;
    }

    void Attack()
    {
        if (handler == null)
            return;
        
        if(handler.IsFiring && fireTime < Time.time)
        {
            fireTime = Time.time + fireDelay;
            Bullet bullet = bulletPool.Borrow();
            bullet.transform.SetPositionAndRotation(fireLocation.position, fireLocation.rotation);
            

        }
        
    }
}

public class Player : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerSteering steering = new PlayerSteering();
    [SerializeField] private PlayerThrottle throttle = new PlayerThrottle();
    private Rigidbody2D _rigidbody2D; 
    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        inputHandler = new PlayerInputHandler(new PlayerControls());
        steering.SetMovementHandler(inputHandler);
        throttle.SetMovementHandler(inputHandler);
    }

    void OnEnable()
    {
        inputHandler.Enable();
    }

    void OnDisable()
    {
        inputHandler.Disable();
    }

    void FixedUpdate()
    {
        float zDeltaRotation = steering.Apply();
        float acceleration = throttle.Apply();
        //Quaternion newRotation = transform.rotation * Quaternion.Euler(0, 0, -zDeltaRotation);
        //Vector3 newPosition = transform.position + velocityDelta;
        //newPosition = EuclideanTorus.current.Wrap(newPosition);
        //transform.SetPositionAndRotation(newPosition, newRotation);
        Debug.Log("FixedUpdate");
        _rigidbody2D.AddTorque(-zDeltaRotation, ForceMode2D.Force);
        Debug.Log(acceleration);
        _rigidbody2D.AddRelativeForce(Vector2.up * acceleration, ForceMode2D.Force);
        _rigidbody2D.position = EuclideanTorus.current.Wrap(_rigidbody2D.position);
        
    }
    
}
