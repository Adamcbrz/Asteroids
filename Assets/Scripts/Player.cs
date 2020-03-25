using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private PlayerInputHandler inputHandler;
    [SerializeField] private PlayerSteering steering = new PlayerSteering();
    [SerializeField] private PlayerThrottle throttle = new PlayerThrottle();
    
    void Awake()
    {
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

    void Update()
    {
        float zDeltaRotation = steering.Apply();
        Vector3 velocityDelta = throttle.Apply(transform.up);
        Quaternion newRotation = transform.rotation * Quaternion.Euler(0, 0, -zDeltaRotation);
        Vector3 newPosition = transform.position + velocityDelta;
        newPosition = EuclideanTorus.current.Wrap(newPosition);
        transform.SetPositionAndRotation(newPosition, newRotation);
    }
    
}
