using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class ApplyVelocitySystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Destroy()
        {
            EventBus.Unsubscribe<ApplyVelocityRequest>(OnVelocity);
        }

        public void Init()
        {
            EventBus.Subscribe<ApplyVelocityRequest>(OnVelocity);
        }

        private void OnVelocity(ApplyVelocityRequest avRequest)
        {
            ref var target = ref avRequest.Target;

            ref var velocity = ref target.Get<VelocityComponent>();
            velocity.Direction = avRequest.Direction;
            velocity.Force = avRequest.Force;

            if (avRequest.IsTemporary)
            {
                ref var temp = ref target.Get<TemporaryVelocityComponent>();
                temp.Duration = avRequest.Duration;
            }
        }
    }
}
