using Gameplay.EnemySystem.BaseEnemy;
using Gameplay.EnemySystem.Data;
using Gameplay.EnemySystem.View;
using UnityEngine;

namespace Gameplay.EnemySystem.Factory
{
    public class EnemyFactory
    {
        public EnemyController Create(Transform spawnPoint, EnemyConfigSO enemyConfigSO)
        {
            var enemyView = Object.Instantiate(enemyConfigSO.ViewPrefab, spawnPoint.position, spawnPoint.rotation);
            var enemyController = CreateEnemy(enemyConfigSO.enemyType, enemyView, enemyConfigSO.Config);
            enemyView.Initialize();
            return enemyController;
        }

        private EnemyController CreateEnemy(EnemyType enemyType, IEnemyView enemyView, EnemyConfig config)
        {
            return enemyType switch
            {
                EnemyType.melee => new MeleeEnemy.MeleeEnemyController(enemyView, config),
                EnemyType.shooter => throw new System.NotImplementedException(),
                _ => throw new System.NotImplementedException(),
            };
        }
    }
}
