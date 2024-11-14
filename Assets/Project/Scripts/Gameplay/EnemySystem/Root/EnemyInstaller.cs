using Gameplay.EnemySystem.Spawn;
using Gameplay.Game;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.DataProviding;
using Infrastructure.TickManagement;
using Leopotam.Ecs;

namespace Gameplay.EnemySystem.Root
{
    public class EnemyInstaller : Installer
    {
        private EnemySpawner enemySpawner;

        public override void PostInitialize()
        {
            var tickManager = ServiceLocator.Get<TickManager>();
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var ecsWorld = ServiceLocator.Get<EcsWorld>();

            var gameConfig = assetProvider.Load<GameConfig>(Constants.GameConfigPath);

            enemySpawner = new EnemySpawner(ecsWorld, gameConfig);
            enemySpawner.Initialize();

            var ecsSystems = ServiceLocator.Get<EcsSystems>();
            ecsSystems.Inject(enemySpawner);
        }
    }
}
