using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{

    #region Unity Exposed Variables

    [SerializeField] private PlayerSteering steering = new PlayerSteering();
    [SerializeField] private PlayerThrottle throttle = new PlayerThrottle();
    [SerializeField] private PlayerGun gun = new PlayerGun();
    [SerializeField] private PlayerDestroyedHandler playerDestroyedHandler = new PlayerDestroyedHandler();

    #endregion

    #region Private Variables
    private PlayerInputHandler inputHandler;
    private Rigidbody2D _rigidbody2D;
    #endregion

    #region Unity Lifecycle

    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        inputHandler = new PlayerInputHandler();
        steering.SetMovementHandler(inputHandler);
        throttle.SetMovementHandler(inputHandler);
        gun.Setup(inputHandler);
        playerDestroyedHandler.Setup(transform);
    }


    void Update()
    {
        inputHandler.Process();
    }

    void FixedUpdate()
    {
        if(playerDestroyedHandler.IsDestroyed)
            return;

        float zDeltaRotation = steering.Apply();
        float acceleration = throttle.Apply();
        _rigidbody2D.AddTorque(-zDeltaRotation, ForceMode2D.Force);
        _rigidbody2D.AddRelativeForce(Vector2.up * acceleration, ForceMode2D.Force);
        _rigidbody2D.position = EuclideanTorus.current.Wrap(_rigidbody2D.position);
        gun.AttemptFire();
    }
    

    void OnTriggerEnter2D(Collider2D collider)
    {
        _rigidbody2D.velocity = Vector2.zero;
        playerDestroyedHandler.Execute();
        Invoke("RestartLevel", 5);
    }

    #endregion

    #region Public Methods
    public void EnableInput()
    {
        inputHandler.Enable();
    }
    #endregion

    #region Private Methods
    void RestartLevel()
    {
        Debug.Log("Restart");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    #endregion
}
