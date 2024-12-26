using Gameplay.EnemySystem.Factory;
using Gameplay.Game.ECS;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.EnemySystem
{
    public class EnemyController : MonoBehaviour, IEnemyController, IEcsEntityHolder
    {
        public int Id { get; private set; }
        public EcsEntity EcsEntity { get; private set; }

        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        private IEnemyFactory enemyFactory;

        public void Initialize(int id, EcsEntity ecsEntity, IEnemyFactory enemyFactory)
        {
            Id = id;
            EcsEntity = ecsEntity;
            this.enemyFactory = enemyFactory;
        }

        public bool IsAlive() => enemyFactory.IsAlive(Id);

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
