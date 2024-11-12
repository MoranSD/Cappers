using Infrastructure.TickManagement;
using Leopotam.Ecs;

namespace Gameplay.Game.ECS
{
    public class EcsSystemsTickableProvider : ITickable
    {
        private readonly EcsSystems systems;

        public EcsSystemsTickableProvider(EcsSystems systems)
        {
            this.systems = systems;
        }

        public void Update(float deltaTime)
        {
            systems.Run();
        }
    }
}
