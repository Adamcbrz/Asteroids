using UnityEngine;
using Unity.Entities;
using Unity.Transforms;
using Unity.Collections;
using Unity.Rendering;
using Unity.Mathematics;

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
    [SerializeField] private Material material;
    [SerializeField] private int numberOfEntities = 10000;
    [SerializeField] private Range speedRange;
    void Start()
    {
        EntityManager entityManager = World.DefaultGameObjectInjectionWorld.EntityManager;

        EntityArchetype entityArchetype = entityManager.CreateArchetype(
            typeof(Translation),
            typeof(RenderMesh),
            typeof(RenderBounds),
            typeof(LocalToWorld),
            typeof(VelocityComponent)
            );

        NativeArray<Entity> entityArray = new NativeArray<Entity>(numberOfEntities, Allocator.Temp);
        entityManager.CreateEntity(entityArchetype, entityArray);
        for (int i = 0; i < entityArray.Length; i++)
        {
            Entity entity = entityArray[i];
            entityManager.SetComponentData(entity, new VelocityComponent { 
                velocity = new float3 (speedRange.Random(), speedRange.Random(), 0)
            });
            //FIXME: I don't like the static reference for EuclideanTorus but will work for now
            entityManager.SetComponentData(entity, new Translation { Value = new float3(EuclideanTorus.current.xRange.Random(), EuclideanTorus.current.yRange.Random(), 0) });
            entityManager.SetSharedComponentData(entity, new RenderMesh { mesh = mesh, material = material });
        }

        entityArray.Dispose();

    }
}
