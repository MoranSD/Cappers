using Gameplay.EnemySystem.Factory;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.EnemySystem.Spawn
{
    public class EnemySpawner
    {
        private readonly EnemyFactory factory;

        private List<EnemyController> spawnedEnemies;

        public EnemySpawner()
        {
            factory = new();
            spawnedEnemies = new();
        }

        public void Initialize()
        {
            var spawnPoints = Object.FindObjectsOfType<EnemySpawnPoint>();

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                var enemy = factory.Create(spawnPoints[i].SpawnPoint, spawnPoints[i].ConfigSO);
                spawnedEnemies.Add(enemy);
            }
        }
    }
}
