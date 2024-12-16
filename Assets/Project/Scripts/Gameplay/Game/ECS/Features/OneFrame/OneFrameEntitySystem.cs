using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class OneFrameEntitySystem : IEcsRunSystem
    {
        private readonly EcsFilter<OneFrameEntity> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                entity.Destroy();
            }
        }
    }
}
