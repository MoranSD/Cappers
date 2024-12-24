using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class AddFollowControlSystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Destroy()
        {
            EventBus.Unsubscribe<AddFollowControlRequest>(OnAddFollow);
        }

        public void Init()
        {
            EventBus.Subscribe<AddFollowControlRequest>(OnAddFollow);
        }

        private void OnAddFollow(AddFollowControlRequest followControlRequest)
        {
            if (followControlRequest.Sender.Has<FollowControllerComponent>() == false) return;

            ref var senderControlComponent = ref followControlRequest.Sender.Get<FollowControllerComponent>();
            ref var controlTarget = ref followControlRequest.Target;

            if (controlTarget.Has<TagAvailableForFollowControlInteraction>() == false)
            {
                Debug.Log("Cant control uncontrollable target");
                return;
            }

            if (senderControlComponent.EntitiesInControl.Contains(controlTarget) == false)
                senderControlComponent.EntitiesInControl.Add(controlTarget);

            ref var followComponent = ref controlTarget.Get<FollowComponent>();
            followComponent.Target = followControlRequest.Sender;

            ref var underfollowTag = ref controlTarget.Get<TagUnderFollowControl>();
            underfollowTag.Owner = followControlRequest.Sender;
        }
    }
}
