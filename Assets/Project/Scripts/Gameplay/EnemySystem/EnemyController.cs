using Gameplay.Game.ECS;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Gameplay.EnemySystem
{
    public class EnemyController : MonoBehaviour, IEnemyController, IEcsEntityHolder
    {
        public int Id { get; private set; }
        public bool IsAlive => this != null && EcsEntity.IsAlive();
        public EcsEntity EcsEntity { get; private set; }

        [field: SerializeField] public NavMeshAgent NavMeshAgent { get; private set; }

        public void Initialize(int id, EcsEntity ecsEntity)
        {
            Id = id;
            EcsEntity = ecsEntity;
        }

        public void Destroy()
        {
            Destroy(gameObject);
        }
    }
}
