using System;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    public Action<Vector3> RaycastHit;
    private Camera _camera;

    private LayerMask LayerMask => LayerManager.Default | LayerManager.Enemy;

    private void Start()
    {
        _camera = FindObjectOfType<Camera>();
    }

    private void Update()
    {
#if UNITY_EDITOR
        if (Input.GetMouseButtonDown(0))
            Raycasting(Input.mousePosition);
#endif
        
        if (Input.touchCount != 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
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