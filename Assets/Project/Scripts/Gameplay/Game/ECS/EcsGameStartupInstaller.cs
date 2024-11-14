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

        public override void PreInitialize()
        {
            tickManager = ServiceLocator.Get<TickManager>();

            world = new EcsWorld();
            systems = new EcsSystems(world);

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
#endif  

            systems.ConvertScene();

            ServiceLocator.Register(world);
            ServiceLocator.Register(systems);
        }

        public override void PostInitialize()
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

                .Add(new ComebackToFollowAfterFightSystem())
                .Add(new AddFollowControlSystem())
                .Add(new RemoveFollowControlSystem())
                .Add(new UnitGoToIdleAfterFollowControlSystem())
                .Add(new UnitGoToIdleAfterFightSystem())

                .Add(new TargetLookSystem())//finds targets around

                .Add(new UpdateAgroTargetSystem())//from look to agro
                .Add(new TargetAgroSetFollowSystem())//set follow
                .Add(new UpdateFollowAgroTargetSystem())//set closest target to follow
                .Add(new TargetAgroAttackSystem())//attack target in attack range

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
                .OneFrame<TryFindAndUpdateFollowControlTargetStateRequest>()
                .OneFrame<RemoveFollowControlRequest>()
                .OneFrame<RemovedFollowControlEvent>()
                .OneFrame<AddFollowControlRequest>()
                .OneFrame<AgentSetDestinationRequest>()
                .OneFrame<ApplyDamageRequest>();
        }
    }
}
