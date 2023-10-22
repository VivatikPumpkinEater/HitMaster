using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using GameObject = UnityEngine.GameObject;

/// <summary>  </summary>
public class LocationLoader
{
    private WaypointsController _waypointsController;
    private LocationContainer _locationContainer;

    public async UniTask Init()
    {
        _locationContainer = Object.FindObjectOfType<LocationContainer>();
        var locationStaticData = JsonConfig.LoadStaticData(_locationContainer.StaticData);
        await CreateLocationObjects(locationStaticData);
    }

    private async UniTask CreateLocationObjects(LocationStaticData locationStaticData)
    {
        var objectsStaticData = locationStaticData.ObjectsData;
        
        var enemies = new Dictionary<string, EnemyController>();
        var waypoints = new Dictionary<WaypointView, List<string>>();

        foreach (var objectStaticData in objectsStaticData)
        {
            var id = objectStaticData.Id;

            switch (objectStaticData)
            {
                case WaypointStaticData waypointStaticData:
                {
                    var go = CreateGameObjectFromData(waypointStaticData, _locationContainer.WaypointTransform);
                    var waypointView = go.AddComponent<WaypointView>();
                    waypoints.Add(waypointView, waypointStaticData.EnemiesID);
                    
                    break;
                }

                case EnemyStaticData enemyStaticData:
                {
                    var go = await Addressables.InstantiateAsync(EnemiesConfig.GetAssetByType(enemyStaticData.Type),
                        enemyStaticData.Position, enemyStaticData.Rotation);
                    var enemyView = go.GetComponent<EnemyView>();
                    var enemyController = new EnemyController(enemyView);
                    enemies.Add(id, enemyController);
                    
                    break;
                }
            }
        }

        var waypointsList = new List<WaypointController>();
        
        foreach (var (waypointView, enemiesIds) in waypoints)
        {
            var waypointEnemies = new List<EnemyController>();

            foreach (var enemyId in enemiesIds)
                waypointEnemies.Add(enemies[enemyId]);
            
            waypointsList.Add(new WaypointController(waypointView, waypointEnemies));
        }

        _waypointsController = new WaypointsController(waypointsList);

        var input = new GameObject("InputHandler").AddComponent<InputHandler>();

        var characterGo = await Addressables.InstantiateAsync(CharacterConfig.GetCharacterAsset(), _locationContainer.StartCharacterPoint);
        var characterView = characterGo.GetComponent<CharacterView>();
        var characterController = new CharacterController(characterView, _waypointsController, input);
    }
    
    private GameObject CreateGameObjectFromData(ObjectStaticData data, Transform parent = null)
    {
        var go = new GameObject(data.Name);
        go.transform.SetParent(parent);
        go.transform.SetLocalPositionAndRotation(data.Position, data.Rotation);
        go.transform.localScale = data.Scale;

        return go;
    }

    private void CreateCamera()
    {
        
    }
}