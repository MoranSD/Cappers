using Gameplay.Ship.UnitControl.Placement;
using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class FollowControlSystem : IEcsRunSystem
    {
        private readonly ShipUnitPlacement placement = null;
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<FollowControlRequest> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var followControlRequest = ref filter.Get1(i);

                ref var senderTransform = ref followControlRequest.Sender.Get<TranslationComponent>().Transform;
                ref var senderUnitControl = ref followControlRequest.Sender.Get<FollowControlComponent>();

                if (EnvironmentProvider.TryGetEntityHoldersAround
                    (senderTransform, followControlRequest.Range, followControlRequest.TargetLayer, out var holdersAround) == false)
                    continue;

                var entityAround = EnvironmentProvider.GetClosestHolder(senderTransform.position, holdersAround).EcsEntity;

                if (senderUnitControl.EntitiesInControl.Contains(entityAround))
                {
                    senderUnitControl.EntitiesInControl.Remove(entityAround);
                    entityAround.Del<FollowComponent>();

                    if (entityAround.Has<TagUnit>())
                    {
                        var entityRequest = _world.NewEntity();
                        ref var destinationRequest = ref entityRequest.Get<AgentSetDestinationRequest>();

                        destinationRequest.Target = entityAround;

                        ref var unitTag = ref entityAround.Get<TagUnit>();
                        destinationRequest.Destination = placement.GetUnitIdlePosition(unitTag.Id);
                    }
                }
                else
                {
                    senderUnitControl.EntitiesInControl.Add(entityAround);
                    ref var followComponent = ref entityAround.Get<FollowComponent>();
                    followComponent.Target = followControlRequest.Sender;
                }
            }
        }
    }
}
