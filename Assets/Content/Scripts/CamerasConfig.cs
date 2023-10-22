    using UnityEngine;
    using UnityEngine.AddressableAssets;

    public class CamerasConfig : BaseConfig<CamerasConfig>
    {
        [SerializeField] private AssetReference _gameplayCamera;

        public static AssetReference GetGameplayCamera()
        {
            return Instance._gameplayCamera;
        }
    }
