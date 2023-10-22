using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

partial class LocationEditor
{
    private const string Waypoint = "Waypoint";

    private WaypointEditor _waypointEditor;
    private bool _isDrawSelectMode;

    private EnemyType _selectedEnemyType;
    private int _waypointIndex;

    private WaypointEditor WaypointEditor => _waypointEditor == null
        ? _waypointEditor = LocationContainer.WaypointTransform.GetComponent<WaypointEditor>()
        : _waypointEditor;
    
    private List<string> WaypointsNames => _waypointEditor.Names;

    private void EditingSceneOnGUI()
    {
        EditorGUILayout.LabelField(LocationName, GUIStyles.TitleLabel);

        // Level editing
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Location elements
        EditorGUILayout.LabelField("Waypoints", GUIStyles.TitleLabel);
        {
            if (GUILayout.Button("Add Waypoint", GUIStyles.YellowButton))
            {
                var go = LevelCreator.CreateGameObject(LocationContainer.WaypointTransform, Waypoint);
                var waypointLocationComponent = go.AddComponent<WaypointLocationComponent>();
                WaypointEditor.Waypoints.Add(waypointLocationComponent);
                WaypointEditor.ImmediatelyUpdateData();
                Selection.activeObject = go;
            }
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
        
        EditorGUILayout.LabelField("Enemies", GUIStyles.TitleLabel);
        {
            OnEnemiesGUI();
        }
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Save data
        EditorGUILayout.BeginHorizontal();
        {
            if (GUILayout.Button("Save location", GUIStyles.GreenButton))
            {
                SaveLocationData();
                SaveScene(SceneManager.GetActiveScene());
            }
        }
        EditorGUILayout.EndHorizontal();

        // Close location
        if (GUILayout.Button("Close scene", GUIStyles.RedButton))
        {
            if (EditorUtility.DisplayDialog("Warning", "Are you sure you want to close the scene?", "Yes", "No"))
                CloseScene();
            else
                CurrentState = EditorState.Editing;
        }
    }

    private void OnEnemiesGUI()
    {
        if (WaypointEditor == null || WaypointsNames == null || WaypointsNames.Count == 0)
        {
            EditorGUILayout.HelpBox("To add enemies, you must create a Waypoint", MessageType.Info);
            return;
        }
        
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

    private void SaveLocationData()
    {
        var locationName = SceneManager.GetActiveScene().name;
        if (!locationName.Contains(PatternLocationName))
        {
            CurrentState = EditorState.Loading;
            Debug.LogError("Save data error! Incorrect location name!");
            return;
        }
        
        var staticData = new LocationStaticData
        {
            ObjectsData = new(),
        };

        var waypoints = LocationContainer.WaypointTransform.GetComponentsInChildren<WaypointLocationComponent>();
        foreach (var waypointLocationComponent in waypoints)
        {
            var data = new ObjectStaticData(waypointLocationComponent);
            var enemiesIds = new List<string>();
            
            foreach (var enemyLocationComponent in waypointLocationComponent.EnemyLocationComponents)
            {
                enemiesIds.Add(enemyLocationComponent.Id);
                var enemyData = new ObjectStaticData(enemyLocationComponent);
                staticData.ObjectsData.Add(new EnemyStaticData(enemyData, enemyLocationComponent.EnemyType));
            }

            staticData.ObjectsData.Add(new WaypointStaticData(data, enemiesIds));
        }
        
        //Save data
        var jsonSettings = JsonConfig.Settings;
        var locationDataJson = JsonConvert.SerializeObject(staticData, jsonSettings);
        var locationDataPath = PathConfig.StaticData(locationName);
        var locationDataAsset = new TextAsset(locationDataJson);
        AssetDatabase.DeleteAsset(locationDataPath);
        AssetDatabase.CreateAsset(locationDataAsset, locationDataPath);

        AssetDatabase.SaveAssets();

        LocationContainer.StaticData = AssetDatabase.LoadAssetAtPath<TextAsset>(locationDataPath);

        Debug.LogWarning("--------------Data saved--------------");
    }

    /// <summary> Сохранить сцену </summary>
    private void SaveScene(Scene scene)
    {
        EditorSceneManager.SaveScene(scene);
    }

    private void CloseScene()
    {
        var locationsComponent = LocationContainer.GetComponentsInChildren<LocationComponent>();
        foreach (var locationComponent in locationsComponent)
        {
            if (locationComponent == null)
                continue;
            DestroyImmediate(locationComponent.gameObject);
        }
        
        DestroyImmediate(WaypointEditor);
        
        var activeScene = SceneManager.GetActiveScene();
        if (!activeScene.name.Contains(PatternLocationName))
        {
            CurrentState = EditorState.Loading;
            return;
        }

        CurrentState = EditorState.Loading;

        SaveScene(activeScene);

        EditorSceneManager.OpenScene(EditorBuildSettings.scenes[0].path, OpenSceneMode.Single);
        Debug.LogWarning("--------------- Scene saved and closed!---------------");
    }
}