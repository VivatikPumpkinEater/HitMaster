using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _direction;
    
    public void Init(Vector3 direction)
    {
        _direction = direction;
    }

    private void Update()
    {
        if (_direction == Vector3.zero)
            return;
        
        transform.Translate(_direction * 15f * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}