using Gameplay.EnemySystem.Factory;
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
        public override void PreInitialize()
        {
            var tickManager = ServiceLocator.Get<TickManager>();
            var assetProvider = ServiceLocator.Get<IAssetProvider>();
            var ecsWorld = ServiceLocator.Get<EcsWorld>();

            var gameConfig = assetProvider.Load<GameConfig>(Constants.GameConfigPath);
            var factoryConfig = assetProvider.Load<EnemyFactoryConfig>(Constants.EnemyFactoryConfig);

            var factory = new EnemyFactory(ecsWorld, gameConfig, factoryConfig);
            ServiceLocator.Register<IEnemyFactory>(factory);

            //spawn by spawnPoints
            var spawnPoints = FindObjectsOfType<EnemySpawnPoint>();
            for (int i = 0; i < spawnPoints.Length; i++)
                factory.Create(spawnPoints[i].SpawnPoint, spawnPoints[i].ConfigSO);
        }

        public override void Dispose()
        {
            ServiceLocator.Remove<IEnemyFactory>();
        }
    }
}
