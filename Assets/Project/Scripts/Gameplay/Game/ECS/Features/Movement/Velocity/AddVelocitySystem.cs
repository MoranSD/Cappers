using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class AddVelocitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<VelocityComponent> filter = null;
        public void Run()
        {
            foreach(var i in filter)
            {
                ref var target = ref filter.GetEntity(i);
                ref var velocity = ref filter.Get1(i);

                EventBus.Invoke(new MoveRequest()
                {
                    Target = target,
                    Direction = velocity.Direction,
                    Speed = velocity.Force,
                });
            }
        }
    }
}
