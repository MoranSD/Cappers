using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class ComebackToFollowAfterAgroSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<EndAgroEvent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var endsAgroEvent = ref filter.Get1(i);
                ref var entity = ref endsAgroEvent.Entity;

                if (entity.Has<TagUnderFollowControl>() == false)
                    continue;

                ref var followOwner = ref entity.Get<TagUnderFollowControl>().Owner;

                _world.NewOneFrameEntity(new AddFollowControlRequest()
                {
                    Sender = followOwner,
                    Target = entity,
                });
            }
        }
    }
}
