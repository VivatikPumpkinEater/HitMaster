using System;

[Serializable]
public class EnemyStaticData : ObjectStaticData
{
    public EnemyType Type;

    public EnemyStaticData()
    {
    }
    
    public EnemyStaticData(ObjectStaticData data, EnemyType type) : base(data)
    {
        Type = type;
    }
}