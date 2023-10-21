using System;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

partial class LocationEditor : EditorWindow
{
    private const string PatternLocationName = "Level";
    
    private EditorState _editorState;
    private LocationContainer _locationContainer;
    
    private WaypointEditor _waypointEditor;
    private EnemiesEditor _enemiesEditor;

    private WaypointEditor WaypointEditor => _waypointEditor == null
        ? _waypointEditor = LocationContainer.WaypointTransform.GetComponent<WaypointEditor>()
        : _waypointEditor;
    
    private EnemiesEditor EnemiesEditor { get; set; }

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

    private void OnEnable()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        SetComponents().Forget();
    }

    private async UniTask SetComponents()
    {
        await UniTask.WaitWhile(() => WaypointEditor != null);
        EnemiesEditor = new EnemiesEditor(WaypointEditor);
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Location Editor", GUIStyles.TitleLabel);
        
        if (Application.isPlaying)
        {
            EditorGUILayout.LabelField("Editor not working in play mode", GUIStyles.TitleLabel);
            return;
        }

        if (CurrentState == EditorState.None)
            CurrentState = LocationName.Contains(PatternLocationName) ? EditorState.Editing : EditorState.Loading;

        if (!LocationName.Contains(PatternLocationName) && CurrentState != EditorState.Creation)
            CurrentState = EditorState.Loading;

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

    private enum EditorState
    {
        None,
        Loading,
        Creation,
        Editing,
    }
}