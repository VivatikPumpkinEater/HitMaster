using UnityEngine;

/// <summary> Вьюшка локации и ее контейнер </summary>
public class LocationContainer : MonoBehaviour
{
    /// <summary> Постоянные данные объектов </summary>
    public TextAsset StaticData;

    /// <summary> Источник света на локации </summary>
    public Light DirectionalLight;

    /// <summary> Стартовая точка перса </summary>
    [Header("Parents")] 
    public Transform StartCharacterPoint;

    /// <summary> Окружение </summary>
    public Transform EnvironmentTransform;

    /// <summary> Родитель всех врагов </summary>
    public Transform EnemiesTransform;
}