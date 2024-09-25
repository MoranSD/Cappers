using Utils;

namespace Gameplay.EnemySystem.Look
{
    public interface IEnemyLookView
    {
        bool TryGetTargetAround(float range, out IAttackTarget attackTarget);
    }
}
