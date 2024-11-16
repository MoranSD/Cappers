using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UpdateAgroFollowTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TranslationComponent, TargetAgroComponent, TargetLookComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var transform = ref filter.Get1(i).Transform;
                ref var agroComponent = ref filter.Get2(i);
                ref var lookComponent = ref filter.Get3(i);

                agroComponent.HasTarget = lookComponent.HasTargetsInRange;
                agroComponent.Target = lookComponent.HasTargetsInRange ? 
                    EntityUtil.GetClosestEntity(transform, lookComponent.Targets) : default;

                if (agroComponent.HasTarget == false) continue;

                ref var entity = ref filter.GetEntity(i);
                ref var followComponent = ref entity.Get<FollowComponent>();
                followComponent.Target = agroComponent.Target;
            }
        }
    }
}
