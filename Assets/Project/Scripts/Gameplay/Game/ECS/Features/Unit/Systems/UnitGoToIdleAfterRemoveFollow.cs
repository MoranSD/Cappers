using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitGoToIdleAfterRemoveFollow : IEcsInitSystem, IEcsDestroySystem
    {
        public void Destroy()
        {
            EventBus.Unsubscribe<RemovedFollowControlEvent>(OnRemoveFollow);
        }

        public void Init()
        {
            EventBus.Subscribe<RemovedFollowControlEvent>(OnRemoveFollow);
        }

        private void OnRemoveFollow(RemovedFollowControlEvent removeEvent)
        {
            ref var entity = ref removeEvent.Target;

            if (entity.Has<TagUnit>() == false)
                return;

            ref var unitTag = ref entity.Get<TagUnit>();
            unitTag.Controller.GoToIdlePosition();
        }
    }
}
