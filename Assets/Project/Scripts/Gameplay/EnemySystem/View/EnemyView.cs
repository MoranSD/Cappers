using Gameplay.Components.Health;
using Gameplay.EnemySystem.BaseEnemy;
using Gameplay.EnemySystem.Fight;
using Gameplay.EnemySystem.Look;
using Gameplay.EnemySystem.Movement;
using UnityEngine;
using Utils;

namespace Gameplay.EnemySystem.View
{
    public class EnemyView : MonoBehaviour, IEnemyView, IAttackTargetView
    {
        public EnemyController Controller { get; private set; }
        public IEnemyMovementView Movement => movement;
        public IEnemyLookView Look => look;
        public IHealthView Health => health;
        public IEnemyFightView Fight => fight;
        public IAttackTarget Target => Controller;

        [SerializeField] private EnemyMovementView movement;
        [SerializeField] private EnemyLookView look;
        [SerializeField] private HealthView health;
        [SerializeField] private EnemyFightView fight;
        [SerializeField] private Collider bodyCollider;

        private Vector3 idlePosition;

        public void Initialize(EnemyController controller)
        {
            Controller = controller;
            idlePosition = transform.position;
            health.Initialize(bodyCollider);
        }

        public Vector3 GetIdlePosition() => idlePosition;
    }
}
