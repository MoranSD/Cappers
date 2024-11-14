using Gameplay.Game;
using Gameplay.Game.ECS.Features;
using Gameplay.UnitSystem.Controller;
using Gameplay.UnitSystem.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.UnitSystem.Factory
{
    public class UnitFactory : IUnitFactory
    {
        private readonly EcsWorld ecsWorld;
        private readonly UnitFactoryConfig config;
        private readonly GameConfig gameConfig;

        public UnitFactory(EcsWorld ecsWorld, UnitFactoryConfig config, GameConfig gameConfig)
        {
            this.ecsWorld = ecsWorld;
            this.config = config;
            this.gameConfig = gameConfig;
        }

        public IUnitController Create(UnitData unitData, Vector3 position)
        {
            var bodyPrefab = config.GetBody(unitData.BodyType);
            var controller = GameObject.Instantiate(bodyPrefab, position, Quaternion.identity);

            var unitEntity = ecsWorld.NewEntity();
            controller.Initialize(ecsWorld, unitEntity, unitData);

            ref var tag = ref unitEntity.Get<TagUnit>();
            tag.Id = unitData.Id;

            ref var translation = ref unitEntity.Get<TranslationComponent>();
            translation.Transform = controller.transform;

            ref var movable = ref unitEntity.Get<AgentMovableComponent>();
            movable.NavMeshAgent = controller.NavMeshAgent;
            controller.NavMeshAgent.speed = unitData.Speed;

            unitEntity.Get<AgentDestinationUpdateTime>();

            ref var targetLook = ref unitEntity.Get<TargetLookComponent>();
            targetLook.TargetLayer = gameConfig.PlayerTargetLayers;
            targetLook.Range = 10;

            ref var agro = ref unitEntity.Get<TargetAgroComponent>();
            agro.AttackDistance = 2;
            agro.AttackRate = 2;
            agro.Damage = unitData.Damage;

            ref var health = ref unitEntity.Get<HealthComponent>();
            health.Health = unitData.Health;

            return controller;
        }
    }
}
