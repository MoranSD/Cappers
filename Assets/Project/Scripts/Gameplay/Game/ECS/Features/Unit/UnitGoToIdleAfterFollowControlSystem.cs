using Gameplay.Ship.UnitControl.Placement;
using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitGoToIdleAfterFollowControlSystem : IEcsRunSystem
    {
        private readonly ShipUnitPlacement placement = null;
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<RemovedFollowControlEvent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var targetEntity = ref filter.Get1(i).Target;

                if (targetEntity.Has<TagUnit>())
                {
                    ref var unitTag = ref targetEntity.Get<TagUnit>();
                    _world.NewEntityWithComponent<AgentSetDestinationRequest>(new()
                    {
                        Target = targetEntity,
                        Destination = placement.GetUnitIdlePosition(unitTag.Id)
                    });
                }
            }
        }
    }
}
