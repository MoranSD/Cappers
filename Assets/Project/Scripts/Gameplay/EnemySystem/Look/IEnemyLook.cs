using Utils;

namespace Gameplay.EnemySystem.Look
{
    public interface IEnemyLook
    {
        bool TryGetTargetAround(float range, out IAttackTarget attackTarget);
    }
}
