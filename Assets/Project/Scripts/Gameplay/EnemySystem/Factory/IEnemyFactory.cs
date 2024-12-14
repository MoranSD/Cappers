using Gameplay.EnemySystem.Data;
using UnityEngine;

namespace Gameplay.EnemySystem.Factory
{
    public interface IEnemyFactory
    {
        IEnemyController CreateBoardingEnemy(Transform spawnPoint);
        IEnemyController Create(Transform spawnPoint, EnemyType type);
        IEnemyController Create(Transform spawnPoint, EnemyConfigSO enemyConfigSO);
        bool IsAlive(int enemyId);
    }
}
