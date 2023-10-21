using System.Collections.Generic;
using System.IO;
using Cysharp.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

partial class LocationEditor
{
    private readonly List<string> _locationNames = new();

    private int _selectedLocationIndex;

    private void OnEnableLoading()
    {
        _selectedLocationIndex = 0;

        _locationNames.Clear();
        _locationNames.AddRange(GetLevelsList());
    }

    private void SceneLoadingOnGUI()
    {
        EditorGUILayout.LabelField("Loading", GUIStyles.TitleLabel);
        GUILayout.BeginHorizontal("", "box");
        {
            GUILayout.BeginVertical();
            {
                // LevelName
                var levelIndex =
                    EditorGUILayout.Popup("Location name:", _selectedLocationIndex, _locationNames.ToArray());
                if (levelIndex != _selectedLocationIndex)
                    _selectedLocationIndex = 0;

                _selectedLocationIndex = levelIndex;

                _newLocationName = _locationNames.Count > 0
                    ? _locationNames[_selectedLocationIndex]
                    : string.Empty;
            }
            GUILayout.EndVertical();

            // Load scene
            if (GUILayout.Button("Load", GUIStyles.YellowButtonTwoMarginsHigh) &&
                !string.IsNullOrEmpty(_newLocationName))
            {
                CurrentState = EditorState.Editing;
                LoadLocation(_newLocationName).Forget();
            }
        }
        GUILayout.EndHorizontal();

        // Create new
        if (GUILayout.Button("Create new level", GUIStyles.YellowButtonTwoMarginsHigh))
            CurrentState = EditorState.Creation;
    }

    private async UniTask LoadLocation(string locationName)
    {
        var path = Path.Combine(PathConfig.ScenesFolder, locationName);
        var scene = EditorSceneManager.OpenScene($"{path}.unity");
        await UniTask.WaitWhile(() => !scene.isLoaded);

        LocationContainer.WaypointTransform.AddComponent<WaypointEditor>();
        
        // Data
        var staticData = JsonConfig.LoadStaticData(LocationContainer.StaticData);
        
        var enemies = new Dictionary<string, EnemyLocationComponent>();
        var waypoints = new Dictionary<WaypointLocationComponent, List<string>>();

        foreach (var objectStaticData in staticData.ObjectsData)
        {
            var id = objectStaticData.Id;

            switch (objectStaticData)
            {
                case WaypointStaticData waypointData:
                {
                    var go = LevelCreator.CreateGameObjectFromData(waypointData, LocationContainer.WaypointTransform);
                    var waypointLocationComponent = go.AddComponent<WaypointLocationComponent>();
                    WaypointEditor.Waypoints.Add(waypointLocationComponent);
                    
                    waypoints.Add(waypointLocationComponent, waypointData.EnemiesID);
                    break;
                }
                
                case EnemyStaticData enemyData:
                {
                    var go = LevelCreator.CreateGameObjectFromData(enemyData);
                    var enemyLocationComponent = go.AddComponent<EnemyLocationComponent>();
                    enemyLocationComponent.Id = id;
                    enemyLocationComponent.EnemyType = enemyData.Type;
                    
                    LevelCreator.CreateEnemyPreview(enemyLocationComponent.transform, enemyData.Type);
                    
                    enemies.Add(id, enemyLocationComponent);
                    break;
                }
                
                default:
                {
                    var go = LevelCreator.CreateGameObject(LocationContainer.EnvironmentTransform, objectStaticData.Name);
                    go.AddComponent<LocationComponent>().Id = objectStaticData.Id;
                    break;
                }
            }
        }

        foreach (var (waypoint, enemiesId) in waypoints)
        {
            foreach (var enemyId in enemiesId)
            {
                var enemy = enemies[enemyId];
                waypoint.EnemyLocationComponents.Add(enemy);
                enemy.transform.parent = waypoint.transform;
            }
        }
        
        WaypointEditor.ImmediatelyUpdateData();
    }

    private List<string> GetLevelsList()
    {
        var list = new List<string>();

        var path = PathConfig.ScenesFolder;
        if (!Directory.Exists(path))
            return list;

        var files = Directory.GetFiles(path, "*.unity", SearchOption.AllDirectories);
        if (files.Length == 0)
            return list;

        foreach (var file in files)
        {
#if UNITY_EDITOR_WIN
            var sceneName = file.Replace($"{path}\\", "").Replace(".unity", "");
#else
            var sceneName = file.Replace($"{path}/", "").Replace(".unity", "");
#endif

            list.Add(sceneName);
        }

        return list;
    }
}