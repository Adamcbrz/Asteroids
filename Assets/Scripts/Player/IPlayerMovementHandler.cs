/// <summary>
/// Interface for handling Movement.
/// Maybe over kill but follows SOLID coding principles so can't hurt
/// </summary>

using UnityEngine;

public interface IPlayerMovementHandler
{
    Vector2 Move { get; }
}
