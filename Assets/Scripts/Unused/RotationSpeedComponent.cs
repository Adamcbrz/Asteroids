﻿using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

[GenerateAuthoringComponent]
public struct RotationSpeedComponent : IComponentData
{
    public float3 axis;
    public float speed;
}

