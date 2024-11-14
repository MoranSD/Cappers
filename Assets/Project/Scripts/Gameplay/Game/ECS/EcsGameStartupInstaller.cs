using Gameplay.Game.ECS.Features;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
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

        public override void PostInitialize()
        {
            tickManager = ServiceLocator.Get<TickManager>();

            world = new EcsWorld();
            systems = new EcsSystems(world);

            systems.ConvertScene();

            ServiceLocator.Register(world);
            ServiceLocator.Register(systems);
        }

        public override void LateInitialize()
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
                .Add(new TFTurnSystem())
                .Add(new FollowControlSystem())
                .Add(new InteractionSystem())
                .Add(new TargetLookSystem())
                .Add(new UpdateAgroTargetSystem())
                .Add(new TargetAgroSetFollowSystem())
                .Add(new UpdateFollowAgroTargetSystem())
                .Add(new TargetAgroAttackSystem())
                .Add(new AgentFollowSystem())
                .Add(new AgentSetDestinationSystem())
                .Add(new ApplyDamageSystem())
                .Add(new UnitDieSystem())
                .Add(new EnemyDieSystem());
        }

        private void AddInjections()
        {
            var assetProvider = ServiceLocator.Get<IAssetProvider>();

            var gameConfig = assetProvider.Load<GameConfig>(Constants.GameConfigPath);

            systems.Inject(gameConfig);
        }

        private void AddOneFrames()
        {
            systems
                .OneFrame<InteractionRequest>()
                .OneFrame<FollowControlRequest>()
                .OneFrame<AgentSetDestinationRequest>()
                .OneFrame<ApplyDamageRequest>();
        }
    }
}
