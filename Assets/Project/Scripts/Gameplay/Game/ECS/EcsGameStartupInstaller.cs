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

        public override void AfterInitialize()
        {
            AddSystems();
            AddInjections();

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

                //velocity
                .Add(new AddVelocitySystem())
                .Add(new TemporaryVelocitySystem())

                .Add(new ChGravitySystem())
                .Add(new ChMovementSystem())

                .Add(new ArcMovementSystem())

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

                .Add(new UnitGoToIdleAfterRemoveFollow())

                //UnitJob
                .Add(new UnitApplyInteractJobSystem())
                .Add(new UnitInteractJobSystem())

                //attack system
                .Add(new ReloadAttackCoolDownSystem())
                .Add(new PreventAttackByCoolDownSystem())

                .Add(new DistanceWeaponAttackSystem())
                .Add(new MeleeWeaponAttackSystem())

                .Add(new AgentFollowSystem())
                .Add(new AgentSetDestinationSystem())

                .Add(new DamageZoneSystem())

                .Add(new ApplyDamageSystem())

                //velocity
                .Add(new ApplyVelocitySystem())

                //SlowDown
                .Add(new ApplySlowDownSystem())
                .Add(new SlowDownSystem())
                .Add(new SmoothRecoverySlowDownSystem())

                .Add(new UnitDieSystem())
                .Add(new EnemyDieSystem())

                .Add(new UpdateAnimationSystem())
                
                .Add(new ChangeStateSystem())
                
                .Add(new OneFrameEntitySystem());
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
    }
}
