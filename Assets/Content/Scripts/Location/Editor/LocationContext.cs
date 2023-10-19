using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;
using UnityEngine.SceneManagement;
using Newtonsoft.Json;

/// <summary> Загрузчик и парсинг уровня </summary>
public class LocationContext
{
    public static readonly JsonSerializerSettings JsonSettings = new()
    {
        NullValueHandling = NullValueHandling.Ignore,
        DefaultValueHandling = DefaultValueHandling.Ignore,
        Formatting = Formatting.None,
        TypeNameHandling = TypeNameHandling.Objects
    };

    public AssetReference SceneReference { get; private set; }
    public SceneInstance SceneInstance { get; private set; }
    public LocationContainer LocationContainer { get; private set; }
    public LocationStaticData LocationStaticData { get; private set; }

    public string LocationName { get; private set; }

    /// <summary> Загрузка локации на уровне </summary>
    public async UniTask<LocationContext> LoadLocation(string levelName)
    {
        LocationName = levelName;
        SceneReference = SceneLoader.GetLevelConfig(levelName);
        if (SceneReference == null)
        {
            Debug.LogError($"[LocationContext] Location {levelName} not found!");
            return null;
        }

        SceneInstance = await Addressables.LoadSceneAsync(SceneReference, LoadSceneMode.Additive);
        SceneManager.SetActiveScene(SceneInstance.Scene);

        // LocationContainer = SceneInstance.Scene.FindObjectOfType<LocationContainer>();
        // LocationStaticData = LoadStaticData(LocationContainer.StaticData);

        return this;
    }

    /// <summary> Загрузить статические данные локации </summary>
    public static LocationStaticData LoadStaticData(TextAsset staticDataAsset)
    {
        var staticData = JsonConvert.DeserializeObject<LocationStaticData>(staticDataAsset.text, JsonSettings);
        if (staticData != null)
            return staticData;

        Debug.LogError("Location Static Data no found");
        return null;
    }

    public UniTask Unload()
    {
        return Addressables
            .UnloadSceneAsync(SceneInstance, UnloadSceneOptions.UnloadAllEmbeddedSceneObjects, false)
            .ToUniTask();
    }
}