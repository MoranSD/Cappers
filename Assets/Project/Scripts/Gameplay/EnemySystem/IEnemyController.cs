namespace Gameplay.EnemySystem
{
    public interface IEnemyController
    {
        int Id { get; }
        void Destroy();
    }
}
