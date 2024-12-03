using Gameplay.EnemySystem.Data;
using UnityEngine;

namespace Gameplay.EnemySystem.Factory
{
    public interface IEnemyFactory
    {
        EnemyController CreateBoardingEnemy(Transform spawnPoint);
        EnemyController Create(Transform spawnPoint, EnemyType type);
        EnemyController Create(Transform spawnPoint, EnemyConfigSO enemyConfigSO);
    }
}
