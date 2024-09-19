using Gameplay.EnemySystem.Animation;
using Gameplay.EnemySystem.Look;
using Gameplay.EnemySystem.Movement;
using UnityEngine;

namespace Gameplay.EnemySystem.View
{
    public interface IEnemyView
    {
        IEnemyMovement Movement { get; }
        IEnemyLook Look { get; }
        IEnemyAnimation Animation { get; }

        Vector3 GetIdlePosition();
    }
}
