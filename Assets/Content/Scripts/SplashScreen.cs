using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SplashScreen : MonoBehaviour
{
    private void Awake()
    {
        Application.targetFrameRate = 120;
        
        LoadMainMenu();
    }

    private async void LoadMainMenu()
    {
        var mainMenuAsset = SceneLoader.GetMainMenu();
        await Addressables.LoadSceneAsync(mainMenuAsset);
    }
}