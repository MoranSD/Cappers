using Gameplay.EnemySystem.Animation;
using Gameplay.EnemySystem.Look;
using Gameplay.EnemySystem.Movement;
using UnityEngine;

namespace Gameplay.EnemySystem.View
{
    public class EnemyView : MonoBehaviour, IEnemyView
    {
        public IEnemyMovement Movement => enemyMovement;
        public IEnemyLook Look => enemyLook;
        public IEnemyAnimation Animation => enemyAnimation;

        [SerializeField] private EnemyMovement enemyMovement;
        [SerializeField] private EnemyLook enemyLook;
        [SerializeField] private EnemyAnimation enemyAnimation;

        private Vector3 idlePosition;

        public void Initialize()
        {
            idlePosition = transform.position;
        }

        public Vector3 GetIdlePosition() => idlePosition;
    }
}
