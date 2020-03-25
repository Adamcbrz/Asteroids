using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Physics;
[System.Serializable]
public struct Range
{
    public float min;
    public float max;

    public float Random()
    {
        return UnityEngine.Random.Range(min, max);
    }

    public override string ToString()
    {
        return string.Format("[Range] ({0}, {1})", min, max);
    }
}


public struct Edges {
    public Range x;
    public Range y;
    public override string ToString()
    {
        return string.Format("[Edge] (x: {0}), (y: {1})", x.ToString(), y.ToString());
    }
}

public class AstroidManager : MonoBehaviour
{
    [SerializeField] private Mesh mesh;
    [SerializeField] private UnityEngine.Material material;
    [SerializeField] private int numberOfEntities = 10000;
    [SerializeField] private Range speedRange;
    [SerializeField] private Range rotationSpeedRange;
    void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(Rotation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld),
            typeof(VelocityComponent),
            typeof(RotationSpeedComponent),
            typeof(Unity.Physics.PhysicsCollider),
            typeof(Unity.Physics.RigidBody)

            );

        NativeArray<Entity> entityArray = new NativeArray<Entity>(numberOfEntities, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);
        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            entityManager.SetComponentData(entity, new VelocityComponent { 
                velocity = new float3 (speedRange.Random(), speedRange.Random(), 0)
            });

            Range range = new Range { min = -1, max = 1 };
            entityManager.SetComponentData(entity, new RotationSpeedComponent
            {
                axis = math.normalize((new float3(range.Random(), range.Random(), range.Random()))),
                speed = rotationSpeedRange.Random()
            });

            //FIXME: I don't like the static reference for EuclideanTorus but will work for now
            Range rotRange = new Range { min = 0, max = 360 };
            Quaternion rot = Quaternion.Euler(rotRange.Random(), rotRange.Random(), rotRange.Random());
            entityManager.SetComponentData(entity, new Translation { Value = new float3(EuclideanTorus.current.xRange.Random(), EuclideanTorus.current.yRange.Random(), 0) });
            entityManager.SetComponentData(entity, new Rotation { Value = (quaternion)rot });

            entityManager.SetSharedComponentData(entity, new RenderMesh { mesh = mesh, material = material });
        }

        entityArray.Dispose();

    }
}
