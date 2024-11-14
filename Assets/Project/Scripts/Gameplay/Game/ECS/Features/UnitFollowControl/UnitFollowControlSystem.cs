using Gameplay.Ship.UnitControl.Placement;
using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitFollowControlSystem : IEcsRunSystem
    {
        private readonly ShipUnitPlacement placement = null;
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<UnitFollowControlRequest> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var followControlRequest = ref filter.Get1(i);

                ref var transform = ref followControlRequest.Target.Get<TranslationComponent>().Transform;
                ref var unitControl = ref followControlRequest.Target.Get<UnitFollowControlComponent>();

                if (EnvironmentProvider.TryGetUnitHoldersAround(transform, followControlRequest.Range, out var holdersAround) == false)
                    continue;

                var unitAround = EnvironmentProvider.GetClosestHolder(transform.position, holdersAround).EcsEntity;

                if (unitControl.UnitsInControl.Contains(unitAround))
                {
                    unitControl.UnitsInControl.Remove(unitAround);
                    unitAround.Del<AgentFollowComponent>();

                    var entityRequest = _world.NewEntity();
                    ref var destinationRequest = ref entityRequest.Get<AgentSetDestinationRequest>();

                    destinationRequest.Target = unitAround;

                    ref var unitTag = ref unitAround.Get<TagUnit>();
                    destinationRequest.Destination = placement.GetUnitIdlePosition(unitTag.Id);
                }
                else
                {
                    unitControl.UnitsInControl.Add(unitAround);
                    ref var followComponent = ref unitAround.Get<AgentFollowComponent>();
                    followComponent.Target = transform;
                }
            }
        }
    }
}
