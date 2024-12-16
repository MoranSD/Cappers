using Gameplay.Ship.UnitControl;
using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitEnterInitialStateWhenEndAgroSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EndAgroEvent> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var beginsAgroEvent = ref filter.Get1(i);
                ref var entity = ref beginsAgroEvent.Entity;

                if (entity.Has<TagUnit>() == false)
                    continue;

                entity.Get<TagAvailableForFollowControlInteraction>();

                if (entity.Has<TagUnderFollowControl>())
                    continue;

                ref var unitTag = ref entity.Get<TagUnit>();
                unitTag.Controller.GoToIdlePosition();
            }
        }
    }
}
