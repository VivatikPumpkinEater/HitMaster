using System;
using UnityEngine;

public class CharacterController : IDisposable
{
    private readonly CharacterView _view;
    private readonly WaypointsController _waypointsController;
    private readonly InputHandler _inputHandler;
    private readonly Pool _pool;

    public CharacterController(CharacterView view, WaypointsController waypointsController, InputHandler inputHandler, Pool pool)
    {
        _view = view;
        _waypointsController = waypointsController;
        _inputHandler = inputHandler;
        _pool = pool;

        _inputHandler.RaycastHit += OnRaycastHit;
        _waypointsController.WaypointChange += MoveNextWaypoint;
    }

    private void MoveNextWaypoint()
    {
        _view.NavMeshAgent.SetDestination(_waypointsController.CurrentWaypoint);
    }

    private void OnRaycastHit(Vector3 targetPosition)
    {
        var direction = (targetPosition - _view.ShootPointPosition).normalized;
        var bullet = _pool.GetFreeElement(_view.ShootPointPosition) as Bullet;
        bullet.InitShoot(direction);
    }

    public void Dispose()
    {
        _inputHandler.RaycastHit -= OnRaycastHit;
        _waypointsController.WaypointChange -= MoveNextWaypoint;
    }
}