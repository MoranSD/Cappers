using Gameplay.EnemySystem.BaseEnemy;
using Gameplay.EnemySystem.Factory;
using Infrastructure.TickManagement;
using System.Collections.Generic;
using UnityEngine;

namespace Gameplay.EnemySystem.Spawn
{
    public class EnemySpawner
    {
        private readonly EnemyFactory factory;
        private readonly TickManager tickManager;

        private List<EnemyController> spawnedEnemys;

        public EnemySpawner(TickManager tickManager)
        {
            factory = new();
            spawnedEnemys = new();
            this.tickManager = tickManager;
        }

        public void Initialize()
        {
            var spawnPoints = Object.FindObjectsOfType<EnemySpawnPoint>();

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                var enemy = factory.Create(spawnPoints[i].SpawnPoint, spawnPoints[i].ConfigSO);
                enemy.Initialize();

                tickManager.Add(enemy);
                spawnedEnemys.Add(enemy);
            }
        }

        public void Dispose()
        {
            foreach (var enemy in spawnedEnemys)
            {
                tickManager.Remove(enemy);
                enemy.Dispose();
            }
        }
    }
}
