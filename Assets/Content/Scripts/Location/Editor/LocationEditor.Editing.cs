using Newtonsoft.Json;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

partial class LocationEditor
{
    private void EditingSceneOnGUI()
    {
        EditorGUILayout.LabelField(LocationName, GUIStyles.TitleLabel);

        // Level editing
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Location elements
        EditorGUILayout.LabelField("Waypoints", GUIStyles.TitleLabel);
        EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);

        // Enemies
        EditorGUILayout.LabelField("Enemies", GUIStyles.TitleLabel);
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