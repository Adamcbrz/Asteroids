using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct VelocityComponent : IComponentData
{
    public float3 velocity;
}
