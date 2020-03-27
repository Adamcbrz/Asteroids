using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

public class AsteroidRotator : ComponentSystem
{
    protected override void OnUpdate()
    {
        //Entities.ForEach((ref Rotation rotation, ref RotationSpeedComponent rotationSpeedComponent) =>
        //{
        //    rotation.Value = math.mul(rotation.Value, quaternion.AxisAngle(rotationSpeedComponent.axis, rotationSpeedComponent.speed * Time.DeltaTime));
        //});
    }
}
