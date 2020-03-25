using UnityEngine;

[System.Serializable]
public class PlayerSteering
{
    private IPlayerMovementHandler handler;
    [SerializeField] private float rotationRate = 10;
    
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
}
