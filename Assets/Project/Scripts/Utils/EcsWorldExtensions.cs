using Gameplay.Game.ECS.Features;
using Leopotam.Ecs;

namespace Utils
{
    public static class EcsWorldExtensions
    {
        public static EcsEntity NewOneFrameEntity<T>(this EcsWorld ecsWorld, params T[] components) where T : struct
        {
            var entity = ecsWorld.NewEntity();
            entity.Get<OneFrameEntity>();

            foreach (var component in components)
                entity.Replace(component);

            return entity;
        }
    }
}
