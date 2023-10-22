using UnityEngine;

public class Bullet : PoolObject
{
    ///Маленький, скромненький костыль
    [SerializeField] private BulletType _bulletType;
    
    private Vector3 _direction;
    private float _speed;
    
    private void Awake()
    {
        _speed = BulletsConfig.GetSpeedByType(_bulletType);
    }

    /// <summary> Инициализация перед выстрелом </summary>
    public void InitShoot(Vector3 direction)
    {
        _direction = direction;
    }

    private void Update()
    {
        if (_direction == Vector3.zero)
            return;
        
        transform.Translate(_direction * _speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        var damageable = other.GetComponent<Damageable>();
        if (damageable)
            damageable.TakeDamage();
        
        _direction = Vector3.zero;
        ReturnToPool();
    }
}