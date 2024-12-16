using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class SetAgroTargetFromTargetLookSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TranslationComponent, TargetAgroComponent, TargetLookComponent> filter = null;
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
                    _world.NewOneFrameEntity(new EndAgroEvent()
                    {
                        Entity = entity
                    });
                }
                else if (hadTarget == false && agroComponent.HasTarget)
                {
                    _world.NewOneFrameEntity(new BeginAgroEvent()
                    {
                        Entity = entity
                    });
                }
            }
        }
    }
}
