using Gameplay.EnemySystem.Data;
using Gameplay.Game;
using Gameplay.Game.ECS.Features;
using Gameplay.UnitSystem.Data;
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

            var enemyEntity = ecsWorld.NewEntity();
            controller.Initialize(id, enemyEntity);

            ref var tag = ref enemyEntity.Get<TagEnemy>();
            tag.Id = id;

            ref var translation = ref enemyEntity.Get<TranslationComponent>();
            translation.Transform = controller.transform;

            ref var movable = ref enemyEntity.Get<AgentMovableComponent>();
            movable.NavMeshAgent = controller.NavMeshAgent;

            enemyEntity.Get<AgentDestinationUpdateTime>();

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
    }
}
