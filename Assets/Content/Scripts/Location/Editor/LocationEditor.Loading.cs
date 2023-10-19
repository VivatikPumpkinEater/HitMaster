using System.Collections.Generic;
using System.IO;
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
                LoadLocation(_newLocationName);
            }
        }
        GUILayout.EndHorizontal();

        // Create new
        if (GUILayout.Button("Create new level", GUIStyles.YellowButtonTwoMarginsHigh))
            CurrentState = EditorState.Creation;
    }

    private void LoadLocation(string locationName)
    {
        var path = Path.Combine(PathConfig.ScenesFolder, locationName);
        EditorSceneManager.OpenScene($"{path}.unity");
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