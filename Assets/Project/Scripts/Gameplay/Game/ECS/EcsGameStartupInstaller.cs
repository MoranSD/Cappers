using Gameplay.CameraSystem;
using Gameplay.Game.ECS.Features;
using Gameplay.Player.Data;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Infrastructure.GameInput;
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
                .Add(new PlayerMovementInputSystem())
                .Add(new PlayerInteractionSystem())

                .Add(new ChMovementPhysicsSystem())
                .Add(new ChMovementSystem())

                .Add(new TFTurnSystem())

                //finds targets around
                .Add(new TargetLookSystem())

                .Add(new PlayerTurnSystem())
                .Add(new PlayerAttackSystem())

                //agro system
                .Add(new SetAgroTargetFromTargetLookSystem())
                .Add(new AgroTargetSystem())

                //unit
                .Add(new UnitRemoveFollowInteractionWhenBeginAgroSystem())
                .Add(new UnitEnterInitialStateWhenEndAgroSystem())

                //follow
                .Add(new ComebackToFollowAfterAgroSystem())
                .Add(new AddFollowControlSystem())
                .Add(new RemoveFollowControlSystem())

                //attack system
                .Add(new ReloadAttackCoolDownSystem())
                .Add(new PreventAttackByCoolDownSystem())

                .Add(new DistanceWeaponAttackSystem())

                .Add(new AgentFollowSystem())
                .Add(new AgentSetDestinationSystem())

                .Add(new ApplyDamageSystem())

                .Add(new UnitDieSystem())
                .Add(new EnemyDieSystem())

                .Add(new UpdateAnimationSystem())
                
                .Add(new ChangeStateSystem());
        }

        private void AddInjections()
        {
            var input = ServiceLocator.Get<IInput>();
            var gameCamera = ServiceLocator.Get<IGameCamera>();
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var gameConfig = assetProvider.Load<GameConfig>(Constants.GameConfigPath);
            var playerConfig = assetProvider.Load<PlayerConfigSO>(Constants.PlayerConfigPath);

            systems
                .Inject(gameCamera)
                .Inject(gameConfig)
                .Inject(input)
                .Inject(playerConfig);
        }

        private void AddOneFrames()
        {
            systems
                .OneFrame<TryFindAndUpdateFollowControlTargetStateRequest>()
                .OneFrame<RemoveFollowControlRequest>()
                .OneFrame<RemovedFollowControlEvent>()
                .OneFrame<AddFollowControlRequest>()
                .OneFrame<AgentSetDestinationRequest>()
                .OneFrame<ApplyDamageRequest>()
                .OneFrame<ApplyDamageEvent>()
                .OneFrame<AttackRequest>()
                .OneFrame<EndAgroEvent>()
                .OneFrame<ChangeStateRequest>();
        }
    }
}
