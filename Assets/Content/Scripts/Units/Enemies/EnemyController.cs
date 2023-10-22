using System;

/// <summary> Контроллер врага </summary>
public class EnemyController : IDisposable
{
    public event Action<EnemyController> Die;
        
    private readonly EnemyView _view;

    public EnemyController(EnemyView view)
    {
        _view = view;

        _view.Damage += OnTakeDamage;
    }

    private void OnTakeDamage()
    {
        Dead();
    }

    private void Dead()
    {
        Die?.Invoke(this);
        
        if (_view.RagdollActivator != null)
            _view.RagdollActivator.Activate();
    }

    public void Dispose()
    {
        _view.Damage += OnTakeDamage;
    }
}