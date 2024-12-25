using Gameplay.EnemySystem.Data;
using Gameplay.Game;
using Gameplay.Game.ECS.Features;
using Gameplay.Player.Data;
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

            ref var health = ref enemyEntity.Get<HealthComponent>();
            health.Health = enemyConfigSO.Config.HealthConfig.StartHealthCount;

            var rangeWeapon = ecsWorld.NewEntity()
                .Replace(new RangeWeaponTag())
                .Replace(new WeaponDamageData()
                {
                    Damage = enemyConfigSO.Config.AttackConfig.Damage
                })
                .Replace(new WeaponAttackDistanceData()
                {
                    AttackDistance = enemyConfigSO.Config.AttackConfig.AttackDistance
                })
                .Replace(new WeaponOwnerData()
                {
                    Owner = enemyEntity
                })
                .Replace(new AttackCoolDownComponent()
                {
                    AttackRate = enemyConfigSO.Config.AttackConfig.AttackRate,
                    AttackCoolDown = 0
                });

            ref var weaponLink = ref enemyEntity.Get<WeaponLink>();
            weaponLink.Weapon = rangeWeapon;

            return controller;
        }

        public IEnemyController CreateBoardingEnemy(Transform spawnPoint) => Create(spawnPoint, EnemyType.melee);

        public bool IsAlive(int enemyId) => aliveIds.Contains(enemyId);

        public void RemoveDeadEnemy(int enemyId) => aliveIds.Remove(enemyId);
    }
}
