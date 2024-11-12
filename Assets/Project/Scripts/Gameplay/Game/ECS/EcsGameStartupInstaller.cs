using Gameplay.Game.ECS.Features;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.TickManagement;
using Leopotam.Ecs;
using Voody.UniLeo;

namespace Gameplay.Game.ECS
{
    public class EcsGameStartupInstaller : Installer
    {
        private EcsWorld world;
        private EcsSystems systems;

        private TickManager tickManager;
        private EcsSystemsTickableProvider systemsTickableProvider;

        public override void Initialize()
        {
            tickManager = ServiceLocator.Get<TickManager>();

            world = new EcsWorld();
            systems = new EcsSystems(world);

            systems.ConvertScene();

            ServiceLocator.Register(world);
            ServiceLocator.Register(systems);
        }

        public override void AfterInitialize()
        {
            AddSystems();
            AddInjections();
            AddOneFrames();

            systems.Init();

            systemsTickableProvider = new EcsSystemsTickableProvider(systems);
            tickManager.Add(systemsTickableProvider);
        }

        public override void Dispose()
        {
            ServiceLocator.Remove<EcsSystems>();
            ServiceLocator.Remove<EcsWorld>();

            tickManager.Remove(systemsTickableProvider);

            systems.Destroy();
            world.Destroy();
        }

        private void AddSystems()
        {
            systems
                .Add(new ChMovementPhysicsSystem())
                .Add(new ChMovementSystem())
                .Add(new InteractionSystem())
                .Add(new TFTurnSystem());
        }

        private void AddInjections()
        {

        }

        private void AddOneFrames()
        {
            systems
                .OneFrame<InteractionEvent>();
        }
    }
}
