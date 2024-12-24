using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class ApplySlowDownSystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Destroy()
        {
            EventBus.Unsubscribe<ApplySlowDownRequest>(OnSlowDown);
        }

        public void Init()
        {
            EventBus.Subscribe<ApplySlowDownRequest>(OnSlowDown);
        }

        private void OnSlowDown(ApplySlowDownRequest slRequest)
        {
            ref var target = ref slRequest.Target;

            if (target.Has<MoveSpeedData>())
            {
                ref var speedData = ref target.Get<MoveSpeedData>();
                bool alreadySlowed = target.Has<SlowDownComponent>();
                ref var slowDownComp = ref target.Get<SlowDownComponent>();

                slowDownComp.Duration = slRequest.Duration;

                if (alreadySlowed == false)
                    slowDownComp.NormalSpeed = speedData.Speed;

                if (slRequest.WithSmoothRecovery)
                {
                    ref var recovery = ref target.Get<SmoothRecoverySlowDownComponent>();
                    recovery.StartDuration = slRequest.Duration;
                    recovery.StartSpeed = slRequest.SlowSpeed;
                }

                speedData.Speed = slRequest.SlowSpeed;
            }
        }
    }
}
