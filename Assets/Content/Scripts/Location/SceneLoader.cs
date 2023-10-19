using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneLoader : BaseConfig<SceneLoader>
{
    [SerializeField] private List<AssetReference> _levels;

    private readonly Dictionary<string, AssetReference> _configs = new();

    protected override void OnInit()
    {
        _configs.Clear();

        foreach (var location in _levels)
        {
            var levelName = location.SubObjectName;
            if (_configs.ContainsKey(levelName))
                continue;

            _configs.Add(levelName, location);
        }
    }

    public static AssetReference GetLevelConfig(string levelName)
    {
        var dict = Instance._configs;
        return dict.ContainsKey(levelName) ? dict[levelName] : null;
    }

    public static AssetReference GetLevelConfig(int index)
    {
        var dict = Instance._configs;
        return dict.Count > index ? dict.Values.ElementAt(index) : null;
    }

    public static AssetReference GetFirstLevelConfig()
    {
        return Instance._levels.Count == 0 ? null : Instance._levels[0];
    }

    public static string GetNextLevelName(string currentLvlName)
    {
        var levelConfigs = Instance._configs;
        var currentLvlIndex = 0;
        foreach (var (levelName, levelConfig) in levelConfigs)
        {
            if (levelName.Equals(currentLvlName))
                break;

            currentLvlIndex++;
        }

        var nextLevelIndex = currentLvlIndex + 1;
        if (nextLevelIndex >= levelConfigs.Count)
            nextLevelIndex = 0;

        return levelConfigs.ElementAt(nextLevelIndex).Key;
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Location/SceneLoader", false, 1)]
    private static void Active()
    {
        UnityEditor.Selection.activeObject = Instance;
    }
#endif
}