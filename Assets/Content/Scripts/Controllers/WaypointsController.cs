using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Контроллер вэйпоинтов </summary>
public class WaypointsController : IDisposable
{
    public event Action Completed;
    public event Action WaypointChange;

    private readonly List<WaypointController> _waypoints;

    public Vector3 CurrentWaypoint { get; private set; }

    public WaypointsController(List<WaypointController> waypoints)
    {
        _waypoints = waypoints;

        foreach (var waypointController in _waypoints)
            waypointController.Completed += OnWaypointCompleted;
    }

    public void OnInit()
    {
        ChangeWaypointPosition();
    }

    private void OnWaypointCompleted(WaypointController waypointController)
    {
        waypointController.Completed -= OnWaypointCompleted;
        _waypoints.Remove(waypointController);
        
        if (_waypoints.Count == 0)
            Completed?.Invoke();
        else
            ChangeWaypointPosition();
    }

    private void ChangeWaypointPosition()
    {
        CurrentWaypoint = _waypoints[0].Position;
        WaypointChange?.Invoke();
    }

    public void Dispose()
    {
        foreach (var waypointController in _waypoints)
        {
            waypointController.Completed -= OnWaypointCompleted;
            waypointController.Dispose();
        }
        
        _waypoints.Clear();
    }
}