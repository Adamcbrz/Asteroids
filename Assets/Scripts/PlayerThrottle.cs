using UnityEngine;

[System.Serializable]
public class PlayerThrottle
{
    IPlayerMovementHandler handler;
    [SerializeField] private float acceleration = 10;
    [SerializeField] private float drag = 1;

    private Vector3 velocity = Vector3.zero;

    public void SetMovementHandler(IPlayerMovementHandler handler)
    {
        this.handler = handler;
    }

    public Vector3 Apply(Vector3 forward)
    {
        if (handler != null)
        {
            if (handler.Move.y > 0)
            {
                velocity += forward * (handler.Move.y * acceleration * Time.deltaTime);
            }
            else
            {
                velocity *= drag; // ( drag * Time.deltaTime);
            }
        }
        return velocity ;
    }
}

