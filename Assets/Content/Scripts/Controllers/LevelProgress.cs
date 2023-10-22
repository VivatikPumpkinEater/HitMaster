using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class LevelProgress : BaseConfig<LevelProgress>
{
    [SerializeField] private int _currentLevel;
    private LocationLoader _locationLoader;
    private LocationLoader LocationLoader => _locationLoader ??= new();

    public static async UniTask LoadCurrentLevel()
    {
        var sceneReference = SceneLoader.GetLocation(Instance._currentLevel);
        await Instance.LoadLevel(sceneReference);
    }

    /// <summary> Загружает следующий уровень, если такого нету загружает мени и сбрасывает прогресс </summary>
    public static async UniTask LoadNextLevel()
    {
        Instance._currentLevel++;
        var sceneReference = SceneLoader.GetLocation(Instance._currentLevel);
        
        if (sceneReference != null)
        {
            await Instance.LoadLevel(sceneReference);
            return;
        }

        Instance._currentLevel = 0;
        await Addressables.LoadSceneAsync(SceneLoader.GetMainMenu());
    }

    private async UniTask LoadLevel(AssetReference sceneAsset)
    {
        await Addressables.LoadSceneAsync(sceneAsset);
        await LocationLoader.Init();
    }
}