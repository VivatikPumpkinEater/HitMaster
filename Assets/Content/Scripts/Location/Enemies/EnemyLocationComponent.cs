using UnityEngine;

public class EnemyLocationComponent : LocationComponent
{
#if UNITY_EDITOR

    [HideInInspector] public EnemyType EnemyType;

#endif
}