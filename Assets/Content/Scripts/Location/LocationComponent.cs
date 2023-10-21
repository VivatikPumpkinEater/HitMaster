using UnityEngine;

/// <summary> Компонент на локации. Используется для редактора локациии </summary>
[ExecuteInEditMode]
public class LocationComponent : MonoBehaviour
{
#if UNITY_EDITOR
    /// <summary> Id объекта </summary>
    public string Id;

    private void OnEnable()
    {
        Id = GetInstanceID().ToString();
    }
#endif
}