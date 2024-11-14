using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class TargetAgroSetFollowSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TargetAgroComponent>.Exclude<FollowComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var agroComponent = ref filter.Get1(i);

                if (agroComponent.HasTarget == false) continue;

                ref var entity = ref filter.GetEntity(i);

                ref var followComponent = ref entity.Get<FollowComponent>();
                followComponent.Target = agroComponent.Target;
            }
        }
    }
}
