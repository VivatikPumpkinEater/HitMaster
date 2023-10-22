using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Pool : MonoBehaviour
{
    private PoolObject _prefab;
    private int _startCopacity;

    private List<PoolObject> _pool = new();

    public async UniTask Init(AssetReference asset, int startCopacity)
    {
        _startCopacity = startCopacity;
        var go = await Addressables.InstantiateAsync(asset);
        var poolObject = go.GetComponent<PoolObject>();
        
        if (poolObject == null)
        {
            Debug.LogError($"{go.name} is not PoolObject!");
            return;
        }
        _pool.Clear();

        _prefab = poolObject;
        poolObject.ReturnToPool();
        _pool.Add(poolObject);
        
        CreatePool();
    }

    private void CreatePool()
    {
        _pool = new List<PoolObject>();

        for (var i = 0; i < _startCopacity; i++)
            CreateElement();
    }

    private PoolObject CreateElement()
    {
        var createObject = Instantiate(_prefab, transform);
        createObject.gameObject.SetActive(false);

        _pool.Add(createObject);

        return createObject;
    }

    private void TryGetElement(out PoolObject element)
    {
        foreach (var i in _pool)
        {
            if (i.gameObject.activeInHierarchy)
                continue;
            
            element = i;
            i.gameObject.SetActive(true);
            return;
        }

        element = CreateElement();
        element.gameObject.SetActive(true);
    }

    /// <summary> Получить эллемент </summary>
    public PoolObject GetFreeElement()
    {
        TryGetElement(out var element);

        return element;
    }

    /// <summary> Получить эллемент в позицию </summary>
    public PoolObject GetFreeElement(Vector3 position)
    {
        var element = GetFreeElement();
        element.transform.position = position;
        return element;
    }

    /// <summary> Получить эллемент в позицию и ротацию референсного трансформа </summary>
    public PoolObject GetFreeElement(Transform referenceTransform)
    {
        var element = GetFreeElement();
        element.transform.SetLocalPositionAndRotation(referenceTransform.position, referenceTransform.rotation);
        return element;
    }

    /// <summary> Получить эллемент в позицию и ротацию </summary>
    public PoolObject GetFreeElement(Vector3 position, Quaternion rotation)
    {
        var element = GetFreeElement(position);
        element.transform.rotation = rotation;
        return element;
    }
}