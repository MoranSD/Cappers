using Gameplay.Ship.UnitControl;
using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitGoToIdleAfterFightSystem : IEcsRunSystem
    {
        private readonly ShipUnitExistenceControl unitExistence = null;
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<EndsAgroEvent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var endsAgroEvent = ref filter.Get1(i);
                ref var entity = ref endsAgroEvent.Entity;

                if (entity.Has<TagUnit>() == false)
                    continue;

                if (entity.Has<TagUnderFollowControl>())
                    continue;

                ref int unitId = ref entity.Get<TagUnit>().Id;

                _world.NewEntityWithComponent<AgentSetDestinationRequest>(new()
                {
                    Target = entity,
                    Destination = unitExistence.GetUnitIdlePosition(unitId)
                });
            }
        }
    }
}
