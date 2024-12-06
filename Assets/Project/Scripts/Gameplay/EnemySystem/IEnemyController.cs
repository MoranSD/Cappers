namespace Gameplay.EnemySystem
{
    public interface IEnemyController
    {
        int Id { get; }
        bool IsAlive { get; }
        void Destroy();
    }
}
