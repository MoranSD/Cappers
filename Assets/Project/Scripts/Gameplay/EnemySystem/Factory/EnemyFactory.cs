using Gameplay.EnemySystem.Data;
using UnityEngine;

namespace Gameplay.EnemySystem.Factory
{
    public class EnemyFactory
    {
        public EnemyController Create(Transform spawnPoint, EnemyConfigSO enemyConfigSO)
        {
            var enemyController = Object.Instantiate(enemyConfigSO.ViewPrefab, spawnPoint.position, spawnPoint.rotation);
            return enemyController;
        }
    }
}
