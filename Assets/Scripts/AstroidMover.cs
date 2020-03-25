using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class AstroidMover : ComponentSystem
{
    protected override void OnUpdate()
    {
        Entities.ForEach((ref Translation translation, ref VelocityComponent velocityComponent) =>
        {
            float3 newPos = translation.Value + velocityComponent.velocity * Time.DeltaTime;
            newPos = EuclideanTorus.current.Wrap(newPos);
            translation.Value = newPos;
        });
    }

}