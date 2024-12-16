using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class SmoothRecoverySlowDownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<SlowDownComponent, SmoothRecoverySlowDownComponent, MoveSpeedData> filter = null;
        private readonly EcsFilter<SmoothRecoverySlowDownComponent>.Exclude<SlowDownComponent> filter2 = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var slowDown = ref filter.Get1(i);
                ref var recovery = ref filter.Get2(i);
                ref var speedData = ref filter.Get3(i);

                var recoveryProgress = 1 - (slowDown.Duration / recovery.StartDuration);
                speedData.Speed = Mathf.Lerp(recovery.StartSpeed, slowDown.NormalSpeed, recoveryProgress);
            }
            foreach (var i in filter2)
            {
                ref var entity = ref filter2.GetEntity(i);
                entity.Del<SmoothRecoverySlowDownComponent>();
            }
        }
    }
}
