using Gameplay.Components.Health;
using Gameplay.EnemySystem.BaseEnemy;
using Gameplay.EnemySystem.Fight;
using Gameplay.EnemySystem.Look;
using Gameplay.EnemySystem.Movement;
using UnityEngine;

namespace Gameplay.EnemySystem.View
{
    public interface IEnemyView
    {
        EnemyController Controller { get; }
        IEnemyMovementView Movement { get; }
        IEnemyLookView Look { get; }
        IHealthView Health { get; }
        IEnemyFightView Fight { get; }

        Vector3 GetIdlePosition();
    }
}
