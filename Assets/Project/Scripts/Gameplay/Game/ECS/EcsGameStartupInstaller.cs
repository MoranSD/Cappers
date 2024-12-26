using Gameplay.CameraSystem;
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
    using Features;

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
                .Add(new ApplyVelocitySystem())
                .Add(new TemporaryVelocitySystem())
                //CC movement
                .Add(new CCGravitySystem())
                .Add(new CCMovementSystem())
                .Add(new CСVelocityMovementSystem())
                //arc
                .Add(new ArcMovementSystem())
                //tf turn
                .Add(new TFTurnSystem())
                //player
                .Add(new PlayerTurnSystem())
                .Add(new PlayerAttackSystem())
                //finds targets around
                .Add(new TargetLookSystem())
                //agro system
                .Add(new UpdateAgroTargetSystem())
                .Add(new UpdateFollowAgroSystem())
                .Add(new AgroTargetAttackSystem())
                //follow
                .Add(new AddFollowControlSystem())
                .Add(new UpdateFollowTargetSystem())
                .Add(new RemoveFollowControlSystem())
                //Unit
                .Add(new UnitSystem())
                //job
                .Add(new UpdateInteractJobTargetFollowSystem())
                .Add(new UnitInteractJobSystem())
                //attack system
                .Add(new ReloadAttackCoolDownSystem())
                .Add(new PreventAttackByCoolDownSystem())
                //weapon
                .Add(new RangeWeaponAttackSystem())
                .Add(new MeleeWeaponAttackSystem())
                //agent
                .Add(new AgentFollowSystem())
                .Add(new AgentSetDestinationSystem())
                //zone
                .Add(new DamageZoneSystem())
                //damage
                .Add(new ApplyDamageSystem())
                //SlowDown
                .Add(new ApplySlowDownSystem())
                .Add(new SlowDownSystem())
                .Add(new SmoothRecoverySlowDownSystem())
                //enemy
                .Add(new EnemyDieSystem())
                //animation
                .Add(new UpdateAnimationSystem())
                //state
                .Add(new ChangeStateSystem())
                //one frame
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
