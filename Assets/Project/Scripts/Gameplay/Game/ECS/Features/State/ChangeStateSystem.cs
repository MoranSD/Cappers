using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class ChangeStateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ChangeStateRequest> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var request = ref filter.Get1(i);
                ref var targetEntity = ref request.Target;

                if(targetEntity.IsAlive() == false)
                    continue;

                request.Delegate.Invoke(ref targetEntity);
            }
        }
    }
}
