using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class UnitGoToIdleAfterRemoveFollow : IEcsRunSystem
    {
        private readonly EcsFilter<RemovedFollowControlEvent> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var removeEvent = ref filter.Get1(i);
                ref var entity = ref removeEvent.Target;

                if (entity.Has<TagUnit>() == false)
                    continue;

                ref var unitTag = ref entity.Get<TagUnit>();
                unitTag.Controller.GoToIdlePosition();
            }
        }
    }
}
