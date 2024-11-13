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

        public UnitFactory(EcsWorld ecsWorld, UnitFactoryConfig config)
        {
            this.ecsWorld = ecsWorld;
            this.config = config;
        }

        public IUnitController Create(UnitData unitData, Vector3 position)
        {
            var bodyPrefab = config.GetBody(unitData.BodyType);
            var controller = GameObject.Instantiate(bodyPrefab, position, Quaternion.identity);

            var unitEntity = ecsWorld.NewEntity();
            controller.Initialize(unitEntity, unitData);

            unitEntity.Get<TagUnit>();

            return controller;
        }
    }
}
