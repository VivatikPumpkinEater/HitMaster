using System;
using System.Collections.Generic;

/// <summary> Контроллер вэйпоинтов </summary>
public class WaypointsController : IDisposable
{
    public event Action Completed;

    private readonly List<WaypointController> _waypoints;

    public WaypointsController(List<WaypointController> waypoints)
    {
        _waypoints = waypoints;

        foreach (var waypointController in _waypoints)
            waypointController.Completed += OnWaypointCompleted;
    }

    private void OnWaypointCompleted(WaypointController waypointController)
    {
        waypointController.Completed -= OnWaypointCompleted;
        _waypoints.Remove(waypointController);
        
        if (_waypoints.Count == 0)
            Completed?.Invoke();
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