using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UpdateAgroTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TranslationComponent, TargetAgroComponent, TargetLookComponent>.Exclude<BlockAgro> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var agroComponent = ref filter.Get2(i);
                ref var lookComponent = ref filter.Get3(i);

                bool hadTarget = agroComponent.HasTarget;

                if (lookComponent.HasTargetsInRange)
                {
                    ref var transform = ref filter.Get1(i).Transform;
                    agroComponent.HasTarget = true;
                    agroComponent.Target = EntityUtil.GetClosestEntity(transform, lookComponent.Targets);
                }
                else if (agroComponent.HasTarget && agroComponent.Target.IsAlive() == false)
                {
                    agroComponent.HasTarget = false;
                    agroComponent.Target = default;
                }

                if (hadTarget && agroComponent.HasTarget == false)
                {
                    ref var entity = ref filter.GetEntity(i);
                    entity.Del<FollowComponent>();
                    entity.Del<TagUnderAgro>();

                    EventBus.Invoke(new EndAgroEvent()
                    {
                        Entity = entity
                    });
                }
                else if (hadTarget == false && agroComponent.HasTarget)
                {
                    ref var entity = ref filter.GetEntity(i);
                    ref var targetTF = ref agroComponent.Target.Get<TranslationComponent>().Transform;

                    ref var follow = ref entity.Get<FollowComponent>();
                    follow.Target = targetTF;

                    entity.Get<TagUnderAgro>();

                    EventBus.Invoke(new BeginAgroEvent()
                    {
                        Entity = entity
                    });
                }
            }
        }
    }
}
