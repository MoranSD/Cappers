using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class AddFollowControlSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AddFollowControlRequest> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var followControlRequest = ref filter.Get1(i);
                ref var senderControlComponent = ref followControlRequest.Sender.Get<FollowControlComponent>();
                ref var controlTarget = ref followControlRequest.Target;

                if(controlTarget.Has<TagAvailableForFollowControlInteraction>() == false)
                {
                    Debug.Log("Cant control uncontrollable target");
                    continue;
                }

                if(senderControlComponent.EntitiesInControl.Contains(controlTarget) == false)
                    senderControlComponent.EntitiesInControl.Add(controlTarget);

                ref var followComponent = ref controlTarget.Get<FollowComponent>();
                followComponent.Target = followControlRequest.Sender;

                ref var underfollowTag = ref controlTarget.Get<TagUnderFollowControl>();
                underfollowTag.Owner = followControlRequest.Sender;
            }
        }
    }
}
