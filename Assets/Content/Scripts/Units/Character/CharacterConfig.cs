using UnityEngine;
using UnityEngine.AddressableAssets;

public class CharacterConfig : BaseConfig<CharacterConfig>
{
    [SerializeField] private AssetReference _characterPrefab;

    public static AssetReference GetCharacterAsset()
    {
        return Instance._characterPrefab;
    }
}