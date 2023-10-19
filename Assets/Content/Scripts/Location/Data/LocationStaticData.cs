using System;
using System.Collections.Generic;

/// <summary> Статические постоянные данные объекта. Не надо хранить в прогрессе </summary>
[Serializable]
public class LocationStaticData
{
    /// <summary> Игровые объекты на сцене </summary>
    public List<ObjectStaticData> ObjectsData;
}