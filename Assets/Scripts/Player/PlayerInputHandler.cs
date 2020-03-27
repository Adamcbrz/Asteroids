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

    #region Public Methods

    public PlayerInputHandler()
    {
        Disable();
    }

    public void Process()
    {
        if (!IsEnabled)
            return;

        Move = new Vector2(Input.GetAxis("Horizontal"), Mathf.Clamp01(Input.GetAxis("Vertical")));
        IsFiring = Input.GetAxis("Fire1") == 1;
    }

    public void Enable()
    {
        IsEnabled = true;
    }

    public void Disable()
    {
        IsEnabled = false;
    }

    #endregion
}