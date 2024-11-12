using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class TFTurnSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TranslationComponent, TFTurnableComponent> filter = null;

        public void Run()
        {
            var deltaTime = Time.deltaTime;

            foreach (var i in filter)
            {
                ref var translation = ref filter.Get1(i);
                ref var turnComponent = ref filter.Get2(i);

                if (turnComponent.Direction == Vector3.zero) continue;

                ref var transform = ref translation.Transform;

                var lookRotation = Quaternion.LookRotation(turnComponent.Direction);
                var targetRotation = Quaternion.RotateTowards(transform.rotation, lookRotation, turnComponent.Speed * deltaTime);
                transform.rotation = targetRotation;
            }
        }
    }
}
