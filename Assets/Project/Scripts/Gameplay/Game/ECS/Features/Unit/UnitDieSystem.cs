using Gameplay.Ship.UnitControl;
using Leopotam.Ecs;
using System.Linq;
namespace Gameplay.Game.ECS.Features
{
    public class UnitDieSystem : IEcsRunSystem
    {
        private readonly ShipUnitExistenceControl existenceControl = null;
        private readonly EcsFilter<TagUnit, HealthComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var unit = ref filter.Get1(i);
                ref var healthComponent = ref filter.Get2(i);

                if (healthComponent.Health > 0) continue;

                int unitId = unit.Id;
                var unitController = existenceControl.ActiveUnits.First(x => x.Data.Id == unitId);
                existenceControl.RemoveUnit(unitId);

                unitController.Destroy();
                ref var unitEntity = ref filter.GetEntity(i);
                unitEntity.Destroy();
            }
        }
    }
}
