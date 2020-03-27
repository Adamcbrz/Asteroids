using UnityEngine;

[System.Serializable]
public class PlayerThrottle
{
    #region Unity Exposed Variables
    
    [SerializeField] private float acceleration = 10;

    #endregion

    #region Private Variables
    
    IPlayerMovementHandler handler;
    Vector3 velocity = Vector3.zero;

    #endregion

    #region Public Methods

    public void SetMovementHandler(IPlayerMovementHandler handler)
    {
        this.handler = handler;
    }

    public float Apply()
    {
        if (handler != null)
        {
            return handler.Move.y * acceleration * Time.deltaTime;
        }

        return 0;
    }

    #endregion
}

