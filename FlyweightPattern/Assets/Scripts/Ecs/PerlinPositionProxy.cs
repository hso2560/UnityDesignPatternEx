using System;
using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class PerlinPositionProxy : MonoBehaviour, IConvertGameObjectToEntity
{
    public void Convert(Entity entity, EntityManager dstManager, GameObjectConversionSystem conversion)
    {
        var data = new PerlinPosition { };
        dstManager.AddComponentData(entity, data);
    }
}
