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

        private void OnAddFollow(AddFollowControlRequest request)
        {
            if (request.Target.Has<BlockFollowControl>())
            {
                Debug.Log("Cant control uncontrollable target");
                return;
            }

            ref var controlTarget = ref request.Target;

            if (request.Sender.Has<FollowControllerComponent>())
            {
                ref var controlComponent = ref request.Sender.Get<FollowControllerComponent>();

                if (controlComponent.EntitiesInControl.Contains(controlTarget) == false)
                    controlComponent.EntitiesInControl.Add(controlTarget);
            }

            ref var ownerTf = ref request.Sender.Get<TranslationComponent>().Transform;

            ref var followComponent = ref controlTarget.Get<FollowComponent>();
            followComponent.Target = ownerTf;

            controlTarget.Get<TagUnderFollowControl>();

            ref var controlled = ref controlTarget.Get<FollowControlledComponent>();
            controlled.Owner = request.Sender;
            controlled.OwnerTransform = ownerTf;
        }
    }
}
