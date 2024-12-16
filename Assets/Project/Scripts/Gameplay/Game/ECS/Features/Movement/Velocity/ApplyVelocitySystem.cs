using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class ApplyVelocitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<ApplyVelocityEvent> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var avEvent = ref filter.Get1(i);
                ref var target = ref avEvent.Target;

                ref var velocity = ref target.Get<VelocityComponent>();
                velocity.Direction = avEvent.Direction;
                velocity.Force = avEvent.Force;

                if(avEvent.IsTemporary)
                {
                    ref var temp = ref target.Get<TemporaryVelocityComponent>();
                    temp.Duration = avEvent.Duration;
                }
            }
        }
    }
}
