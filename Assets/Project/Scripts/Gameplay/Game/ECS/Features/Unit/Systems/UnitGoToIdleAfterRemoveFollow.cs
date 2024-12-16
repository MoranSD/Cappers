using Gameplay.Ship.UnitControl;
using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitGoToIdleAfterRemoveFollow : IEcsRunSystem
    {
        private readonly ShipUnitExistenceControl unitExistence = null;
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<RemovedFollowControlEvent> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var removeEvent = ref filter.Get1(i);
                ref var entity = ref removeEvent.Target;

                if (entity.Has<TagUnit>() == false)
                    continue;

                ref var unitTag = ref entity.Get<TagUnit>();

                _world.NewOneFrameEntity(new AgentSetDestinationRequest()
                {
                    Target = entity,
                    Destination = unitExistence.GetUnitIdlePosition(unitTag.Controller.Data.Id)
                });
            }
        }
    }
}
