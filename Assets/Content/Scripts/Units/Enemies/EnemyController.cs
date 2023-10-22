using System;

/// <summary> Контроллер врага </summary>
public class EnemyController
{
    public event Action<EnemyController> Die;
        
    private readonly EnemyView _view;

    public EnemyController(EnemyView view)
    {
        _view = view;
    }
}