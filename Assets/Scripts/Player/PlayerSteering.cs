using UnityEngine;

[System.Serializable]
public class PlayerSteering
{

    #region Unity Exposed Variable
    [SerializeField] private float rotationRate = 10;
    #endregion

    #region Private Variables
    private IPlayerMovementHandler handler;
    #endregion

    #region Public Methods
    
    public void SetMovementHandler(IPlayerMovementHandler handler)
    {
        this.handler = handler;
    }

    public float Apply()
    {
        if (handler == null)
            return 0;


        return handler.Move.x * rotationRate * Time.deltaTime;
        

    }

    #endregion
}
