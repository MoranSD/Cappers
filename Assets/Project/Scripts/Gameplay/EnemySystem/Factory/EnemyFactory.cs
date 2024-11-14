using Gameplay.EnemySystem.Data;
using Gameplay.Game;
using Gameplay.Game.ECS.Features;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.EnemySystem.Factory
{
    public class EnemyFactory
    {
        private readonly EcsWorld ecsWorld;
        private readonly GameConfig gameConfig;

        public EnemyFactory(EcsWorld ecsWorld, GameConfig gameConfig)
        {
            this.ecsWorld = ecsWorld;
            this.gameConfig = gameConfig;
        }

        public EnemyController Create(Transform spawnPoint, EnemyConfigSO enemyConfigSO, int id)
        {
            var controller = Object.Instantiate(enemyConfigSO.ViewPrefab, spawnPoint.position, spawnPoint.rotation);

            var unitEntity = ecsWorld.NewEntity();
            controller.Initialize(id, unitEntity);

            ref var tag = ref unitEntity.Get<TagEnemy>();
            tag.Id = id;

            ref var movable = ref unitEntity.Get<AgentMovableComponent>();
            movable.NavMeshAgent = controller.NavMeshAgent;

            unitEntity.Get<AgentDestinationUpdateTime>();

            ref var targetLook = ref unitEntity.Get<TargetLookComponent>();
            targetLook.TargetLayer = gameConfig.EnemyTargetLayers;
            targetLook.Range = enemyConfigSO.Config.LookConfig.VisionRange;

            ref var agro = ref unitEntity.Get<TargetAgroComponent>();
            agro.AttackDistance = enemyConfigSO.Config.AttackConfig.AttackDistance;
            agro.AttackRate = enemyConfigSO.Config.AttackConfig.AttackRate;
            agro.Damage = enemyConfigSO.Config.AttackConfig.Damage;

            ref var health = ref unitEntity.Get<HealthComponent>();
            health.Health = enemyConfigSO.Config.HealthConfig.StartHealthCount;

            return controller;
        }
    }
}
