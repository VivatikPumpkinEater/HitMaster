using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneLoader : BaseConfig<SceneLoader>
{
    [SerializeField] private List<AssetReference> _locations;

    public static void AddLocation(AssetReference assetReference)
    {
        Instance._locations.Add(assetReference);
    }
    
    public static AssetReference GetLocation(int index)
    {
        return Instance._locations.Count < index ? null : Instance._locations[index];
    }

    public static AssetReference GetFirstLocation()
    {
        return Instance._locations.Count == 0 ? null : Instance._locations[0];
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Location/SceneLoader", false, 1)]
    private static void Active()
    {
        UnityEditor.Selection.activeObject = Instance;
    }
#endif
}