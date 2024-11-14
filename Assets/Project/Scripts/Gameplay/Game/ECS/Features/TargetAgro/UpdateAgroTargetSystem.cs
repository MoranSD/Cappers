using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UpdateAgroTargetSystem : IEcsRunSystem
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
                agroComponent.Target = EntityUtil.GetClosestEntity(transform, lookComponent.Targets);
            }
        }
    }
}
