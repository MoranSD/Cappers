using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class TemporaryVelocitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<TemporaryVelocityComponent, VelocityComponent> filter = null;
        public void Run()
        {
            var deltaTime = Time.deltaTime;
            foreach (var i in filter)
            {
                ref var temp = ref filter.Get1(i);
                temp.Duration -= Time.deltaTime;

                if (temp.Duration > 0) continue;

                ref var entity = ref filter.GetEntity(i);
                entity.Del<VelocityComponent>();
                entity.Del<TemporaryVelocityComponent>();
            }
        }
    }
}
