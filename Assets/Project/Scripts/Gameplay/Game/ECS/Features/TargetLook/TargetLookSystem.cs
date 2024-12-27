using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class TargetLookSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TranslationComponent, TargetLookComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var transform = ref filter.Get1(i).Transform;
                ref var targetLook = ref filter.Get2(i);

                var targets = EnvironmentProvider
                    .TryGetEntitiesAround(transform, targetLook.Range, targetLook.TargetLayer);

                targetLook.HasTargetsInRange = targets != null;
                targetLook.Targets = targets;
            }
        }
    }
}
