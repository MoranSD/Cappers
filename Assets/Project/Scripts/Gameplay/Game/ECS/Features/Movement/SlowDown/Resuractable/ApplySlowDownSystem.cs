using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class ApplySlowDownSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ApplySlowDownEvent> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var slEvent = ref filter.Get1(i);
                ref var target = ref slEvent.Target;

                if (target.Has<MoveSpeedData>())
                {
                    ref var speedData = ref target.Get<MoveSpeedData>();
                    bool alreadySlowed = target.Has<SlowDownComponent>();
                    ref var slowDownComp = ref target.Get<SlowDownComponent>();

                    slowDownComp.Duration = slEvent.Duration;
                    
                    if(alreadySlowed == false)
                        slowDownComp.NormalSpeed = speedData.Speed;

                    if (slEvent.WithSmoothRecovery)
                    {
                        ref var recovery = ref target.Get<SmoothRecoverySlowDownComponent>();
                        recovery.StartDuration = slEvent.Duration;
                        recovery.StartSpeed = slEvent.SlowSpeed;
                    }

                    speedData.Speed = slEvent.SlowSpeed;
                }
            }
        }
    }
}
