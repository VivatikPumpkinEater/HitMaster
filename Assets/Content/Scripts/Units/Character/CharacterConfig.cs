using UnityEngine;
using UnityEngine.AddressableAssets;

public class CharacterConfig : BaseConfig<CharacterConfig>
{
    [SerializeField] private AssetReferenceT<CharacterView> _characterPrefab;

    [Header("Settings")]
    [SerializeField] private float _movementSpeed;

    public static float MovementSpeed => Instance._movementSpeed;
    
    public static AssetReferenceT<CharacterView> GetCharacterAsset()
    {
        return Instance._characterPrefab;
    }
}