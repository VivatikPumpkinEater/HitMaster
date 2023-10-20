using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class LevelProgressController
{
    private int _currentLevel;
    
    public LevelProgressController()
    {
    }

    public void Test(int i)
    {
        _currentLevel = i;
        LoadLevel().Forget();
    }

    private async UniTask LoadLevel()
    {
        var assetReference = SceneLoader.GetLocation(_currentLevel);
        var sceneInstance = await Addressables.LoadSceneAsync(assetReference);
    }
}