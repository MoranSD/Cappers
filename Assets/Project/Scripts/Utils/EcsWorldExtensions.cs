using Leopotam.Ecs;

namespace Utils
{
    public static class EcsWorldExtensions
    {
        public static EcsEntity NewEntityWithComponent<T>(this EcsWorld ecsWorld, in T component) where T : struct
        {
            var entity = ecsWorld.NewEntity();
            entity.Replace(component);
            return entity;
        }
    }
}
