using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class UnitRemoveFollowInteractionWhenBeginAgroSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BeginAgroEvent> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var beginsAgroEvent = ref filter.Get1(i);
                ref var entity = ref beginsAgroEvent.Entity;

                if (entity.Has<TagUnit>() == false)
                    continue;

                entity.Del<TagAvailableForFollowControlInteraction>();
            }
        }
    }
}
