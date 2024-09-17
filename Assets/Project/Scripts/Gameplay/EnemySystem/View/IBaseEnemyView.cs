using Gameplay.EnemySystem.Animation;
using Gameplay.EnemySystem.Movement;

namespace Gameplay.EnemySystem.View
{
    public interface IBaseEnemyView
    {
        IEnemyMovement Movement { get; }
        IEnemyAnimation Animation { get; }
    }
}
