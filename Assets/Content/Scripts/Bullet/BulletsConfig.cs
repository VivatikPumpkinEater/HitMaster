using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class BulletsConfig : BaseConfig<BulletsConfig>
{
    [SerializeField] private List<string> _bulletTypes;
    [SerializeField] private List<Model> _models;

    private Dictionary<BulletType, Model> _bullets = new();

#if UNITY_EDITOR

    /// <summary> Список моделей для редактора </summary>
    public List<Model> Models => _models;

#endif

    protected override void OnInit()
    {
        foreach (var model in _models)
        {
            if (_bullets.ContainsKey(model.Type))
                continue;

            _bullets.Add(model.Type, model);
        }
    }

    public static AssetReference GetAssetByType(BulletType type)
    {
        if (!Instance._bullets.ContainsKey(type))
            return null;

        return Instance._bullets[type].Asset;
    }

    public static float GetSpeedByType(BulletType type)
    {
        return Instance._bullets[type].Speed;
    }

    public void GenerateTypes()
    {
        TypesGenerator.Generate(true, nameof(BulletType), _bulletTypes);
    }

    [Serializable]
    public class Model
    {
        public BulletType Type;
        public AssetReference Asset;
        public float Speed;
    }
}