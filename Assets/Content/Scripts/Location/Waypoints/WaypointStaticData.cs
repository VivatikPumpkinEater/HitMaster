using System;
using System.Collections.Generic;

[Serializable]
public class WaypointStaticData : ObjectStaticData
{
    public List<string> EnemiesID;

    public WaypointStaticData()
    {
    }
    
    public WaypointStaticData(ObjectStaticData data, List<string> enemiesId) : base(data)
    {
        EnemiesID = enemiesId;
    }
}