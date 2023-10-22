using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class EnemiesConfig : BaseConfig<EnemiesConfig>
{
    [SerializeField] private List<string> _enemiesTypes;
    [SerializeField] private List<Model> _models;

    private Dictionary<EnemyType, AssetReference> _enemies = new();

#if UNITY_EDITOR
    
    /// <summary> Список моделей для редактора </summary>
    public List<Model> Models => _models;
    
#endif
    
    protected override void OnInit()
    {
        foreach (var model in _models)
        {
            if(_enemies.ContainsKey(model.Type))
                continue;
            
            _enemies.Add(model.Type, model.Asset);
        }
    }

    public static AssetReference GetAssetByType(EnemyType type)
    {
        if (!Instance._enemies.ContainsKey(type))
            return null;
        
        return Instance._enemies[type];
    }

    public void GenerateTypes()
    {
        TypesGenerator.Generate(true, nameof(EnemyType), _enemiesTypes);
    }

    [Serializable]
    public class Model
    {
        public EnemyType Type;
        public AssetReference Asset;
    }
}