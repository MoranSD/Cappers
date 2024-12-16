using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class ArcMovementSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TranslationComponent, ArcMovementComponent> filter = null;

        public void Run()
        {
            float deltaTime = Time.deltaTime;

            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                ref var transform = ref filter.Get1(i).Transform;
                ref var arc = ref filter.Get2(i);

                if(arc.Progress < 1)
                {
                    arc.Progress += Time.deltaTime / arc.Duration;

                    if (arc.Progress > 1)
                    {
                        arc.Progress = 1;

                        _world.NewOneFrameEntity(new ReachArcEndEvent()
                        {
                            Entity = entity,
                        });
                    }

                    var targetPosition = Vector3.Lerp(arc.Start, arc.End, arc.Progress);
                    targetPosition += Vector3.up * arc.HighCurve.Evaluate(arc.Progress);
                    transform.position = targetPosition;
                }
            }
        }
    }
}
