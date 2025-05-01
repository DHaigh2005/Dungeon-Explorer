namespace DungeonExplorer
{
    public interface IDamageable
    {
        void ReceiveDamage(int damage);
        bool NotDead();
    }
}