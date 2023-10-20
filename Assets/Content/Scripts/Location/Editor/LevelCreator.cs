using Unity.VisualScripting;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;
using UnityEngine.Windows;

public static class LevelCreator
{
    public const string LocationParentName = "Location";
    public const string EnvironmentParentName = "Environment";
    public const string StartCharacterPointName = "StartCharacterPoint";
    public const string EnemiesParentName = "Enemies";

    /// <summary> Создать сцену </summary>
    public static void Create(string locationName)
    {
        var scene = EditorSceneManager.NewScene(NewSceneSetup.EmptyScene);
        scene.name = locationName;

        InitScene(scene);
    }

    /// <summary> Сцена создана, инициализировать сцену  </summary>
    private static void InitScene(Scene scene)
    {
        // DirectionalLight
        var directionalLightGameObject = new GameObject("DirectionalLight", typeof(Light));
        directionalLightGameObject.transform.rotation = Quaternion.Euler(new Vector3(50, -30, 0));
        var directionalLight = directionalLightGameObject.GetComponent<Light>();
        directionalLight.type = LightType.Directional;
        directionalLight.color = new Color32(255, 244, 214, 255);
        directionalLight.lightmapBakeType = LightmapBakeType.Realtime;
        directionalLight.intensity = 1;

        directionalLight.shadows = LightShadows.Soft;
        directionalLight.shadowStrength = 1;
        directionalLight.shadowResolution = LightShadowResolution.FromQualitySettings;
        directionalLight.shadowBias = 0.05f;
        directionalLight.shadowNormalBias = 0.4f;
        directionalLight.shadowNearPlane = 0.2f;

        directionalLight.cookieSize = 10;
        directionalLight.renderMode = LightRenderMode.Auto;

        // RenderSettings
        RenderSettings.skybox = AssetDatabase.GetBuiltinExtraResource<Material>("Default-Skybox.mat");
        RenderSettings.sun = directionalLight;
        RenderSettings.subtractiveShadowColor = new Color(0.42f, 0.48f, 0.63f);

        RenderSettings.ambientMode = AmbientMode.Skybox;
        RenderSettings.ambientIntensity = 1;

        RenderSettings.defaultReflectionMode = DefaultReflectionMode.Skybox;
        RenderSettings.defaultReflectionResolution = 128;
        RenderSettings.reflectionIntensity = 1;

        RenderSettings.fog = false;
        RenderSettings.fogColor = new Color(0.75f, 0.78f, 0.8f);
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = 35;
        RenderSettings.fogEndDistance = 60;

        RenderSettings.haloStrength = 0.5f;
        RenderSettings.flareFadeSpeed = 3;
        RenderSettings.flareStrength = 1.0f;

        var locationName = scene.name;

        CreateLocationPrefab(directionalLight);

        var levelSceneFolder = $"{PathConfig.ScenesFolder}";
        if (!Directory.Exists(levelSceneFolder))
            Directory.CreateDirectory(levelSceneFolder);

        var scenePath = $"{levelSceneFolder}/{locationName}.unity";
        EditorSceneManager.SaveScene(scene, scenePath);

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }

    private static void CreateLocationPrefab(Light light)
    {
        var locationTr = CreateGameObject(null, LocationParentName).transform;
        var location = locationTr.AddComponent<LocationContainer>();
        location.DirectionalLight = light;

        // StartCharacterPoint
        var startPoint = CreateGameObject(locationTr, StartCharacterPointName).transform;
        location.StartCharacterPoint = startPoint;

        // Environments
        var environmentsParent = CreateGameObject(locationTr, EnvironmentParentName).transform;
        location.EnvironmentTransform = environmentsParent;

        // Enemies
        location.EnemiesTransform = CreateGameObject(locationTr, EnemiesParentName).transform;
    }

    /// <summary> Создать объект </summary>
    public static GameObject CreateGameObject(Transform parent, string name)
    {
        var go = new GameObject(name);
        go.transform.SetParent(parent);
        go.transform.SetLocalPositionAndRotation(Vector3.zero, Quaternion.identity);
        go.transform.localScale = Vector3.one;

        return go;
    }
}