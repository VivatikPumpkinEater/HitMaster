using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class LevelProgressController
{
    private readonly LocationLoader _locationLoader;
    private int _currentLevel;
    
    public LevelProgressController()
    {
        _locationLoader = new LocationLoader();
    }

    public void Test(int i)
    {
        _currentLevel = i;
        LoadLevel().Forget();
    }

    private async UniTask LoadLevel()
    {
        var assetReference = SceneLoader.GetLocation(_currentLevel);
        var scene = await Addressables.LoadSceneAsync(assetReference);
        await _locationLoader.Init();
    }
}