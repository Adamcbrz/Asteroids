using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
using Unity.Mathematics;
using Unity.Physics;
using Collider = Unity.Physics.Collider;
using MeshCollider = Unity.Physics.MeshCollider;

public class AstroidManager : MonoBehaviour
{
    [SerializeField] private RenderMesh displayMesh;
    [SerializeField] private int numberOfEntities = 10000;
    [SerializeField] private Range speedRange;
    [SerializeField] private Range rotationSpeedRange;
    [SerializeField] private float mass = 10;
    [SerializeField] private float3 linearVelocity;
    [SerializeField] private float3 angularVelocity;

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
            typeof(PhysicsCollider),
            typeof(PhysicsVelocity),
            typeof(PhysicsMass),
            typeof(PhysicsGravityFactor),
            typeof(PhysicsDamping)

            );


        BlobAssetReference<Collider> meshCollider = MeshToCollider(displayMesh.mesh);
        //Unity.Physics.MeshCollider.Create(new NativeArray<float3>(displayMesh.mesh.vertexCount, ))
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
            entityManager.SetSharedComponentData(entity, new RenderMesh { mesh = displayMesh.mesh, material = displayMesh.material});
            entityManager.SetComponentData(entity, new PhysicsCollider { Value = meshCollider});
            //Collider* colliderPtr = (Collider*)meshCollider.GetUnsafePtr();
            //entityManager.SetComponentData(entity, PhysicsMass.CreateDynamic(colliderPtr->MassProperties, mass));
            //float3 angularVelocityLocal = math.mul(math.inverse(colliderPtr->MassProperties.MassDistribution.Transform.rot), angularVelocity);
            entityManager.SetComponentData(entity, new PhysicsVelocity()
            {
                Linear = linearVelocity,
                Angular = angularVelocity
            });
            entityManager.SetComponentData(entity, new PhysicsDamping()
            {
                Linear = 0.01f,
                Angular = 0.05f
            });
        }

        entityArray.Dispose();

    }

    BlobAssetReference<Collider> MeshToCollider(Mesh mesh)
    {
        var vertices = new NativeList<float3>(mesh.vertexCount, Allocator.Temp);
        var triangles = new NativeList<int3>(mesh.triangles.Length / 3, Allocator.Temp);
        for(int i = 0; i < mesh.vertexCount; i++)
        {
            vertices.Add((float3)mesh.vertices[i]);
        }

        for (int i = 0; i < mesh.triangles.Length; i += 3)
        {
            triangles.Add(new int3(mesh.triangles[i], mesh.triangles[i + 1], mesh.triangles[i + 2]));
        }

        return MeshCollider.Create(vertices, triangles);
    }
}
