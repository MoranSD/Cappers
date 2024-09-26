using Gameplay.Components.Health;
using Gameplay.EnemySystem.Fight;
using Gameplay.EnemySystem.Look;
using Gameplay.EnemySystem.Movement;
using UnityEngine;

namespace Gameplay.EnemySystem.View
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        public IEnemyMovementView Movement => movement;
        public IEnemyLookView Look => look;
        public IHealthView Health => health;
        public IEnemyFightView Fight => fight;

        [SerializeField] private EnemyMovementView movement;
        [SerializeField] private EnemyLookView look;
        [SerializeField] private HealthView health;
        [SerializeField] private EnemyFightView fight;
        [SerializeField] private Collider bodyCollider;

        private Vector3 idlePosition;

        public void Initialize()
        {
            idlePosition = transform.position;
            health.Initialize(bodyCollider);
        }

        public Vector3 GetIdlePosition() => idlePosition;
    }
}
