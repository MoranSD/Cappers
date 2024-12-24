using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class RemoveFollowControlSystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Destroy()
        {
            EventBus.Unsubscribe<RemoveFollowControlRequest>(OnRemoveFollow);
        }

        public void Init()
        {
            EventBus.Subscribe<RemoveFollowControlRequest>(OnRemoveFollow);
        }

        private void OnRemoveFollow(RemoveFollowControlRequest removeFollowControlRequest)
        {
            if (removeFollowControlRequest.Sender.Has<FollowControllerComponent>() == false) return;

            ref var senderControlComponent = ref removeFollowControlRequest.Sender.Get<FollowControllerComponent>();
            ref var controlTarget = ref removeFollowControlRequest.Target;

            if (controlTarget.Has<TagUnderFollowControl>() == false)
            {
                Debug.Log("Cant remove control from not controlled target");
                return;
            }

            senderControlComponent.EntitiesInControl.Remove(controlTarget);
            controlTarget.Del<FollowComponent>();
            controlTarget.Del<TagUnderFollowControl>();

            EventBus.Invoke(new RemovedFollowControlEvent()
            {
                Target = controlTarget,
            });
        }
    }
}
