using System;
using UnityEngine;

/// <summary> Данные объекта на карте </summary>
[Serializable]
public class ObjectStaticData
{
    /// <summary> GetInstance ID </summary>
    [HideInInspector] public string Id;
    
    /// <summary> Имя объекта </summary>
    [HideInInspector] public string Name;
    
    /// <summary> Глобальная позиция </summary>
    [HideInInspector] public Vector3 Position;

    /// <summary> Глобальный поворот объекта </summary>
    [HideInInspector] public Quaternion Rotation;

    /// <summary> Локальный размер объекта </summary>
    [HideInInspector] public Vector3 Scale;

    protected ObjectStaticData()
    {
    }

    public ObjectStaticData(LocationComponent locationComponent)
    {
        var locCompTransform = locationComponent.transform;

        Id = locationComponent.Id;
        Name = locationComponent.name;
        Position = locCompTransform.position;
        Rotation = locCompTransform.rotation;
        Scale = locCompTransform.localScale;
    }

    protected ObjectStaticData(ObjectStaticData data)
    {
        Id = data.Id;
        Name = data.Name;
        Position = data.Position;
        Rotation = data.Rotation;
        Scale = data.Scale;
    }
}