using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class EnemiesEditor
{
    private readonly WaypointEditor _waypointEditor;
    private List<string> WaypointsNames => _waypointEditor.Names;

    private bool _isDrawSelectMode;

    private EnemyType _selectedEnemyType;
    private int _waypointIndex;

    public EnemiesEditor(WaypointEditor waypointEditor)
    {
        _waypointEditor = waypointEditor;
    }

    public void OnGUI()
    {
        if (_waypointEditor == null || WaypointsNames.Count == 0)
            return;
        
        EditorGUILayout.BeginVertical();
        {
            _waypointIndex = EditorGUILayout.Popup("Waypoints names", _waypointIndex, WaypointsNames.ToArray());
            _selectedEnemyType = (EnemyType)EditorGUILayout.EnumPopup("EnemyType:", _selectedEnemyType);

            if (GUILayout.Button("Create", GUIStyles.YellowButtonTwoMarginsHigh))
            {
                var parentTr = GameObject.Find(WaypointsNames[_waypointIndex])?.transform;
                if (parentTr == null)
                    return;

                var waypointLocationComponent = parentTr.GetComponent<WaypointLocationComponent>();
                var go = LevelCreator.CreateGameObject(parentTr, _selectedEnemyType.ToString());
                var enemyLocationComponent = go.AddComponent<EnemyLocationComponent>();
                enemyLocationComponent.EnemyType = _selectedEnemyType;
                waypointLocationComponent.EnemyLocationComponents.Add(enemyLocationComponent);
                
                LevelCreator.CreateEnemyPreview(enemyLocationComponent.transform, _selectedEnemyType);
                
                Selection.activeObject = go;
            }
        }
        EditorGUILayout.EndVertical();
    }
}