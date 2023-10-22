using UnityEngine;

public abstract class PoolObject : MonoBehaviour
{
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
}