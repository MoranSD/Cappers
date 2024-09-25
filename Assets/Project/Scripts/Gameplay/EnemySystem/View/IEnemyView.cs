using Gameplay.EnemySystem.Fight;
using Gameplay.EnemySystem.Health;
using Gameplay.EnemySystem.Look;
using Gameplay.EnemySystem.Movement;
using UnityEngine;

namespace Gameplay.EnemySystem.View
{
    public interface IEnemyView
    {
        IEnemyMovementView Movement { get; }
        IEnemyLookView Look { get; }
        IEnemyHealthView Health { get; }
        IEnemyFightView Fight { get; }

        Vector3 GetIdlePosition();
    }
}
