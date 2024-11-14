using Gameplay.EnemySystem.Factory;
using Gameplay.Game;
using Leopotam.Ecs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gameplay.EnemySystem.Spawn
{
    public class EnemySpawner
    {
        public IReadOnlyList<IEnemyController> ActiveEnemies => spawnedEnemies;

        private readonly EnemyFactory factory;

        private List<IEnemyController> spawnedEnemies;

        public EnemySpawner(EcsWorld ecsWorld, GameConfig gameConfig)
        {
            factory = new(ecsWorld, gameConfig);
            spawnedEnemies = new();
        }

        public void Initialize()
        {
            var spawnPoints = Object.FindObjectsOfType<EnemySpawnPoint>();

            for (int i = 0; i < spawnPoints.Length; i++)
            {
                var enemy = factory.Create(spawnPoints[i].SpawnPoint, spawnPoints[i].ConfigSO, i);
                spawnedEnemies.Add(enemy);
            }
        }

        public void RemoveEnemy(int id)
        {
            var enemy = spawnedEnemies.First(x => x.Id == id);
            spawnedEnemies.Remove(enemy);
        }
    }
}
