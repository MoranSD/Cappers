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

        private void OnRemoveFollow(RemoveFollowControlRequest request)
        {
            if (request.Target.Has<BlockFollowControl>())
            {
                Debug.Log("Cant control uncontrollable target");
                return;
            }
            if (request.Target.Has<TagUnderFollowControl>() == false)
            {
                Debug.Log("Cant remove control from not controlled target");
                return;
            }

            ref var controlTarget = ref request.Target;

            if (request.Sender.Has<FollowControllerComponent>())
            {
                ref var senderControlComponent = ref request.Sender.Get<FollowControllerComponent>();
                senderControlComponent.EntitiesInControl.Remove(controlTarget);
            }

            controlTarget.Del<FollowComponent>();
            controlTarget.Del<TagUnderFollowControl>();
            controlTarget.Del<FollowControlledComponent>();

            EventBus.Invoke(new RemovedFollowControlEvent()
            {
                Target = controlTarget,
            });
        }
    }
}
