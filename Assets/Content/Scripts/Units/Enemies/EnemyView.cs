public class EnemyView : Damageable
{
    public RagdollActivator RagdollActivator { get; private set; }

    private void Awake()
    {
        // Если нету компанента будет null и не нужно всегда проверять
        RagdollActivator = GetComponent<RagdollActivator>();
    }
}