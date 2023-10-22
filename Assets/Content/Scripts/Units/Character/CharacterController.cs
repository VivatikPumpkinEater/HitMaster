using System;
using UnityEngine;

public class CharacterController : IDisposable
{
    private readonly CharacterView _view;
    private readonly WaypointsController _waypointsController;
    private readonly InputHandler _inputHandler;

    public CharacterController(CharacterView view, WaypointsController waypointsController, InputHandler inputHandler)
    {
        _view = view;
        _waypointsController = waypointsController;
        _inputHandler = inputHandler;

        _inputHandler.RaycastHit += OnRaycastHit;
    }

    private void MoveNext()
    {
        _view.NavMeshAgent.SetDestination(_waypointsController.CurrentWaypoint);
    }

    private void OnRaycastHit(Vector3 targetPosition)
    {
        
    }

    public void Dispose()
    {
        _inputHandler.RaycastHit -= OnRaycastHit;
    }
}