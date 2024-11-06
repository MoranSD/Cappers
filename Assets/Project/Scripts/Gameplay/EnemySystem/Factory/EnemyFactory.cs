using Gameplay.EnemySystem.BaseEnemy;
using Gameplay.EnemySystem.Data;
using UnityEngine;

namespace Gameplay.EnemySystem.Factory
{
    public class EnemyFactory
    {
        public EnemyController Create(Transform spawnPoint, EnemyConfigSO enemyConfigSO)
        {
            var enemyView = Object.Instantiate(enemyConfigSO.ViewPrefab, spawnPoint.position, spawnPoint.rotation);
            var enemyController = new EnemyController(enemyView, enemyConfigSO.Config);
            enemyView.Initialize(enemyController);
            return enemyController;
        }

        /*
        private EnemyController CreateEnemy(EnemyType enemyType, IEnemyView enemyView, EnemyConfig config)
        {
            return enemyType switch
            {
                EnemyType.melee => new MeleeEnemy.MeleeEnemyController(enemyView, config),
                EnemyType.shooter => throw new System.NotImplementedException(),
                _ => throw new System.NotImplementedException(),
            };
        }
         */
    }
}
