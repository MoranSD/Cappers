using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class UpdateFollowTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TagUnderFollowControl, FollowControlledComponent>.Exclude<BlockFollowControl> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var follow = ref filter.GetEntity(i).Get<FollowComponent>();
                ref var tag = ref filter.Get2(i);

                follow.Target = tag.OwnerTransform;
            }
        }
    }
}
