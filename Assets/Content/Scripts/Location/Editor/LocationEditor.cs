using System;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

partial class LocationEditor : EditorWindow
{
    private const string PatternLocationName = "Level";
    
    private EditorState _editorState;
    private LocationContainer _locationContainer;

    private static string LocationName => SceneManager.GetActiveScene().name;

    private EditorState CurrentState
    {
        get => _editorState;
        set
        {
            if (CurrentState == value)
                return;
            
            _editorState = value;
            
            if (CurrentState == EditorState.Loading)
                OnEnableLoading();
        }
    }
    
    private LocationContainer LocationContainer
    {
        get
        {
            if (_locationContainer != null)
                return _locationContainer;

            _locationContainer = FindObjectOfType<LocationContainer>();
            return _locationContainer;
        }
    }
    
    /// <summary> Показать окно с эдитором </summary>
    [MenuItem("Location/LocationEditor")]
    private static void ShowWindow()
    {
        var window = CreateInstance<LocationEditor>();
        window.minSize = new(400, 50);
        window.titleContent = new("LevelEditor");
        window.Show();
    }

    private void OnGUI()
    {
        GUILayout.Label("Location Editor", GUIStyles.WindowTitleStyle);
        GUILayout.Label("", GUI.skin.horizontalSlider);
        
        if (Application.isPlaying)
        {
            EditorGUILayout.LabelField("Editor not working in play mode", GUIStyles.TitleLabel);
            return;
        }

        if (CurrentState == EditorState.None)
            CurrentState = LocationName.Contains(PatternLocationName) ? EditorState.Editing : EditorState.Loading;

        if (!LocationName.Contains(PatternLocationName) && CurrentState != EditorState.Creation)
            CurrentState = EditorState.Loading;

        GUILayout.Space(15);
        HeaderOnGUI();
        GUILayout.Space(15);
        
        try
        {
            switch (CurrentState)
            {
                case EditorState.Creation:
                {
                    CreateSceneOnGUI();
                    break;
                }
                case EditorState.Loading:
                {
                    SceneLoadingOnGUI();
                    break;
                }
                case EditorState.Editing:
                {
                    EditingSceneOnGUI();
                    break;
                }
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error: " + ex.Message + ". Please open the Splash scene and load the level " +
                           "through the LevelEditor.");
        }
    }
    
    private void HeaderOnGUI()
    {
        EditorGUILayout.BeginHorizontal();
        {
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("SceneLoader", GUIStyles.YellowButton, GUILayout.Width(120)))
                SceneLoader.Active();

            if (GUILayout.Button(EditorGUIUtility.IconContent("d__Help@2x"), GUIStyles.MiniButton))
                Application.OpenURL(PathConfig.HelpUrl);
        }
        EditorGUILayout.EndHorizontal();
    }


    private enum EditorState
    {
        None,
        Loading,
        Creation,
        Editing,
    }
}