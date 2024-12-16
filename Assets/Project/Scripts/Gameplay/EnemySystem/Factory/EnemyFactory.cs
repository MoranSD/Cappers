using Gameplay.EnemySystem.Data;
using Gameplay.Game;
using Gameplay.Game.ECS.Features;
using Leopotam.Ecs;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.EnemySystem.Factory
{
    public class EnemyFactory : IEnemyFactory
    {
        private readonly EcsWorld ecsWorld;
        private readonly GameConfig gameConfig;
        private readonly EnemyFactoryConfig config;

        private int globalSpawnId = 0;

        private List<int> aliveIds = new List<int>();

        public EnemyFactory(EcsWorld ecsWorld, GameConfig gameConfig, EnemyFactoryConfig config)
        {
            this.ecsWorld = ecsWorld;
            this.gameConfig = gameConfig;
            this.config = config;
        }

        public IEnemyController Create(Transform spawnPoint, EnemyType type)
        {
            EnemyConfigSO typeConfig = config.GetDefaultConfig(type);
            return Create(spawnPoint, typeConfig);
        }

        public IEnemyController Create(Transform spawnPoint, EnemyConfigSO enemyConfigSO)
        {
            var controller = Object.Instantiate(enemyConfigSO.ViewPrefab, spawnPoint.position, spawnPoint.rotation);

            var enemyEntity = ecsWorld.NewEntity();
            aliveIds.Add(globalSpawnId);
            controller.Initialize(globalSpawnId, enemyEntity);
            globalSpawnId++;

            ref var tag = ref enemyEntity.Get<TagEnemy>();
            tag.Controller = controller;

            ref var translation = ref enemyEntity.Get<TranslationComponent>();
            translation.Transform = controller.transform;

            ref var movable = ref enemyEntity.Get<AgentMovableComponent>();
            movable.NavMeshAgent = controller.NavMeshAgent;

            enemyEntity.Get<AgentDestinationUpdateTimeData>();

            ref var targetLook = ref enemyEntity.Get<TargetLookComponent>();
            targetLook.TargetLayer = gameConfig.EnemyTargetLayers;
            targetLook.Range = enemyConfigSO.Config.LookConfig.VisionRange;

            ref var agro = ref enemyEntity.Get<TargetAgroComponent>();
            //agro.AttackDistance = enemyConfigSO.Config.AttackConfig.AttackDistance;
            //agro.AttackRate = enemyConfigSO.Config.AttackConfig.AttackRate;
            //agro.Damage = enemyConfigSO.Config.AttackConfig.Damage;

            ref var health = ref enemyEntity.Get<HealthComponent>();
            health.Health = enemyConfigSO.Config.HealthConfig.StartHealthCount;

            var weaponEntity = ecsWorld.NewEntity();

            ref var distanceComponent = ref weaponEntity.Get<DistanceWeaponComponent>();
            distanceComponent.AttackDistance = enemyConfigSO.Config.AttackConfig.AttackDistance;
            distanceComponent.Damage = enemyConfigSO.Config.AttackConfig.Damage;

            ref var coolDownComponent = ref weaponEntity.Get<AttackCoolDownComponent>();
            coolDownComponent.AttackRate = enemyConfigSO.Config.AttackConfig.AttackRate;
            coolDownComponent.AttackCoolDown = 0;

            ref var ownerComponent = ref weaponEntity.Get<WeaponOwnerComponent>();
            ownerComponent.Owner = enemyEntity;

            ref var weaponLink = ref enemyEntity.Get<WeaponLink>();
            weaponLink.Weapon = weaponEntity;

            return controller;
        }

        public IEnemyController CreateBoardingEnemy(Transform spawnPoint) => Create(spawnPoint, EnemyType.melee);

        public bool IsAlive(int enemyId) => aliveIds.Contains(enemyId);

        public void RemoveDeadEnemy(int enemyId) => aliveIds.Remove(enemyId);
    }
}
