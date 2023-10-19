using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;

public class LevelProgressController
{
    private int _index;
    
    public LevelProgressController()
    {
    }

    private async UniTask LoadLevel()
    {
        var assetReference = SceneLoader.GetLevelConfig(_index);
        await Addressables.LoadSceneAsync(assetReference);   
    }
}