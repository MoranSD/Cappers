using Gameplay.Components.Health;
using Gameplay.EnemySystem.Fight;
using Gameplay.EnemySystem.Look;
using Gameplay.EnemySystem.Movement;
using UnityEngine;

namespace Gameplay.EnemySystem.View
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        public IEnemyMovementView Movement => enemyMovement;
        public IEnemyLookView Look => enemyLook;
        public IHealthView Health => enemyHealth;
        public IEnemyFightView Fight => enemyFight;

        [SerializeField] private EnemyMovementView enemyMovement;
        [SerializeField] private EnemyLookView enemyLook;
        [SerializeField] private HealthView enemyHealth;
        [SerializeField] private EnemyFightView enemyFight;

        private Vector3 idlePosition;

        public void Initialize()
        {
            idlePosition = transform.position;
        }

        public Vector3 GetIdlePosition() => idlePosition;
    }
}
