using Gameplay.EnemySystem.Spawn;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.TickManagement;

namespace Gameplay.EnemySystem.Root
{
    public class EnemyInstaller : Installer
    {
        private EnemySpawner enemySpawner;

        public override void Initialize()
        {
            var tickManager = ServiceLocator.Get<TickManager>();

            enemySpawner = new EnemySpawner(tickManager);
            enemySpawner.Initialize();
        }

        public override void Dispose()
        {
            enemySpawner.Dispose();
        }
    }
}
