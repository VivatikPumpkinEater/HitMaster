using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Action<Vector3> RaycastHit;
    private Camera _camera;

    private LayerMask LayerMask => LayerManager.Default | LayerManager.Enemy;

    private void Awake()
    {
        _camera = Camera.main;
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButton(0))
            Raycasting(Input.mousePosition);
#endif
        
        if (Input.touchCount != 0)
            Raycasting(Input.GetTouch(0).position);
    }

    private void Raycasting(Vector3 touchPosition)
    {
        RaycastHit hit;
        var ray = _camera.ScreenPointToRay(touchPosition);
        
        if (Physics.Raycast(ray, out hit, LayerMask))
            RaycastHit?.Invoke(hit.point);
    }
}