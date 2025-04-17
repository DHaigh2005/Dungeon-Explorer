namespace DungeonExplorer
{
    public interface IDamageable
    {
        void RecieveDamage(int damage);
        bool NotDead();
    }
}