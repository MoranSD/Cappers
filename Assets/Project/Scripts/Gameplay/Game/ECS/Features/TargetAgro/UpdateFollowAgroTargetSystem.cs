using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class UpdateFollowAgroTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TargetAgroComponent, FollowComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var agroComponent = ref filter.Get1(i);
                ref var followComponent = ref filter.Get2(i);
                ref var entity = ref filter.GetEntity(i);

                if (agroComponent.HasTarget == false) continue;

                followComponent.Target = agroComponent.Target;
            }
        }
    }
}
