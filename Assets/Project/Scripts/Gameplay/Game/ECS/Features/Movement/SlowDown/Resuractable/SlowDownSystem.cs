using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class SlowDownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SlowDownComponent, MoveSpeedData> filter = null;
        public void Run()
        {
            float deltaTime = Time.deltaTime;
            foreach (var i in filter)
            {
                ref var slowDown = ref filter.Get1(i);

                slowDown.Duration -= deltaTime;
                if (slowDown.Duration > 0) continue;

                ref var speedData = ref filter.Get2(i);
                speedData.Speed = slowDown.NormalSpeed;

                ref var entity = ref filter.GetEntity(i);
                entity.Del<SlowDownComponent>();
            }
        }
    }
}
