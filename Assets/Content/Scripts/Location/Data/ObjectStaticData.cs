using System;
using UnityEngine;

/// <summary> Данные объекта на карте </summary>
[Serializable]
public abstract class ObjectStaticData
{
    /// <summary> GetInstance объекта </summary>
    [HideInInspector] public string Id;

    /// <summary> Ссылка на адресабл объекта </summary>
    [HideInInspector] public string Path;

    /// <summary> Глобальная позиция </summary>
    [HideInInspector] public Vector3 Position;

    /// <summary> Глобальный поворот объекта </summary>
    [HideInInspector] public Quaternion Rotation;

    /// <summary> Локальный размер объекта </summary>
    [HideInInspector] public Vector3 Scale;

    protected ObjectStaticData()
    {
    }

    protected ObjectStaticData(ObjectStaticData data)
    {
        Id = data.Id;
        Path = data.Path;
        Position = data.Position;
        Rotation = data.Rotation;
        Scale = data.Scale;
    }
}