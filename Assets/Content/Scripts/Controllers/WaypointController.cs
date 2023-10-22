﻿using System;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Контроллер вэйпоинта </summary>
public class WaypointController : IDisposable
{
    /// <summary> Все враги данного вэйпоинта уничтожены </summary>
    public event Action<WaypointController> Completed;
    
    private readonly List<EnemyController> _enemies;
    private readonly WaypointView _view;

    public Vector3 Position => _view.transform.position;

    public WaypointController(WaypointView view, List<EnemyController> enemies)
    {
        _enemies = enemies;

        foreach (var enemy in _enemies)
            enemy.Die += OnEnemyDie;
    }

    private void OnEnemyDie(EnemyController enemyController)
    {
        enemyController.Die -= OnEnemyDie;
        _enemies.Remove(enemyController);
        
        if (_enemies.Count == 0)
            Completed?.Invoke(this);
    }
    

    public void Dispose()
    {
        foreach (var enemy in _enemies)
            enemy.Die -= OnEnemyDie;
        
        _enemies.Clear();
    }
}