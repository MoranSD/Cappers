using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class SetAgroTargetFromTargetLookSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TranslationComponent, TargetAgroComponent, TargetLookComponent>.Exclude<BlockFreezed> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                ref var transform = ref filter.Get1(i).Transform;
                ref var agroComponent = ref filter.Get2(i);
                ref var lookComponent = ref filter.Get3(i);

                bool hadTarget = agroComponent.HasTarget;

                agroComponent.HasTarget = lookComponent.HasTargetsInRange;
                agroComponent.Target = lookComponent.HasTargetsInRange ?
                    EntityUtil.GetClosestEntity(transform, lookComponent.Targets) : default;

                if (hadTarget && agroComponent.HasTarget == false)
                {
                    EventBus.Invoke(new EndAgroEvent()
                    {
                        Entity = entity
                    });
                }
                else if (hadTarget == false && agroComponent.HasTarget)
                {
                    EventBus.Invoke(new BeginAgroEvent()
                    {
                        Entity = entity
                    });
                }
            }
        }
    }
}
