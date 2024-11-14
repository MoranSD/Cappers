using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class RemoveFollowControlSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<RemoveFollowControlRequest> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var removeFollowControlRequest = ref filter.Get1(i);
                ref var senderControlComponent = ref removeFollowControlRequest.Sender.Get<FollowControlComponent>();
                ref var controlTarget = ref removeFollowControlRequest.Target;

                senderControlComponent.EntitiesInControl.Remove(controlTarget);
                controlTarget.Del<FollowComponent>();
                controlTarget.Del<TagUnderFollowControl>();

                var requestEntity = _world.NewEntity();
                ref var removedEvent = ref requestEntity.Get<RemovedFollowControlEvent>();
                removedEvent.Target = controlTarget;
            }
        }
    }
}
