using System;
using UnityEngine;
using static UnityEngine.InputSystem.InputAction;

public class PlayerInputHandler : IPlayerMovementHandler, IPlayerAttackingHandler
{
    private PlayerControls controls;

    public bool IsFiring { get; protected set; }
    public Vector2 Move { get; protected set; }
    public PlayerInputHandler(PlayerControls controls)
    {
        this.controls = controls;

        controls.Player.Fire.started += OnFireDown;
        controls.Player.Fire.canceled += OnFireUp;
        controls.Player.Fire.performed += OnFireUp;
        controls.Player.Move.performed += OnMove;
        controls.Player.Move.canceled += OnMove;
    }

    private void OnMove(CallbackContext ctx)
    {
        Move = ctx.ReadValue<Vector2>();
        
    }

    private void OnFireUp(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        IsFiring = false;
    }

    private void OnFireDown(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        IsFiring = true;
    }

    public void Enable()
    {
        controls.Enable();
    }

    public void Disable()
    {
        controls.Disable();
    }
}