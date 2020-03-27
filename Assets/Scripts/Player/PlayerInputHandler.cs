using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : IPlayerMovementHandler, IPlayerAttackingHandler
{
    #region Properties

    public bool IsEnabled { get; protected set; }
    public bool IsFiring { get; protected set; }
    public Vector2 Move { get; protected set; }

    #endregion

    #region Private Variables

    private PlayerControls controls;

    #endregion

    #region Public Methods

    public PlayerInputHandler()
    {
        controls =  new PlayerControls();
        controls.Enable();

        controls.Player.Fire.started += _ => { IsFiring = true; };
        controls.Player.Fire.canceled += _ => { IsFiring = false; };

        Disable();
    }

    public void Process()
    {
        Debug.Log("Process");
        if (!IsEnabled)
            return;
        
        Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));
        Debug.Log("Horizontal: " + controls.Player.Move.ReadValue<Vector2>());
        Move = controls.Player.Move.ReadValue<Vector2>();
    }

    public void Enable()
    {
        IsEnabled = true;
        controls.Player.Move.Enable();
        controls.Player.Enable();
    }

    public void Disable()
    {
        IsEnabled = false;
        controls.Player.Disable();
    }

    #endregion
}