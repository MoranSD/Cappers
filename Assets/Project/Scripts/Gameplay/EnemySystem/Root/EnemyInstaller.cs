using Gameplay.EnemySystem.Spawn;
using Infrastructure;
using Infrastructure.Composition;
using Infrastructure.TickManagement;

namespace Gameplay.EnemySystem.Root
{
    public class EnemyInstaller : Installer
    {
        private EnemySpawner enemySpawner;

        public override void PostInitialize()
        {
            var tickManager = ServiceLocator.Get<TickManager>();

            enemySpawner = new EnemySpawner();
            enemySpawner.Initialize();
        }
    }
}
