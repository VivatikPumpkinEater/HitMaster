public static class LayerManager
{
    public static int Default => 1 << 0;
    public static int TransparentFX => 1 << 1;
    public static int IgnoreRaycast => 1 << 2;
    public static int Water => 1 << 4;
    public static int UI => 1 << 5;
    public static int Player => 1 << 6;
    public static int Bullet => 1 << 7;
    public static int Enemy => 1 << 10;
    public static int EnemyRagdoll => 1 << 11;
}