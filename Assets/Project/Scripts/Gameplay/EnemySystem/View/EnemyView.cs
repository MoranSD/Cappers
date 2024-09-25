using Gameplay.EnemySystem.Fight;
using Gameplay.EnemySystem.Health;
using Gameplay.EnemySystem.Look;
using Gameplay.EnemySystem.Movement;
using UnityEngine;

namespace Gameplay.EnemySystem.View
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        public IEnemyMovementView Movement => enemyMovement;
        public IEnemyLookView Look => enemyLook;
        public IEnemyHealthView Health => enemyHealth;
        public IEnemyFightView Fight => enemyFight;

        [SerializeField] private EnemyMovementView enemyMovement;
        [SerializeField] private EnemyLookView enemyLook;
        [SerializeField] private EnemyHealthView enemyHealth;
        [SerializeField] private EnemyFightView enemyFight;

        private Vector3 idlePosition;

        public void Initialize()
        {
            idlePosition = transform.position;
        }

        public Vector3 GetIdlePosition() => idlePosition;
    }
}
