namespace Gameplay.EnemySystem
{
    public interface IEnemyController
    {
        int Id { get; }
        bool IsAlive();
        void Destroy();
    }
}
