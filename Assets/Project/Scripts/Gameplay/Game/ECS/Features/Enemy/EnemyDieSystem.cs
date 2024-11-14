using Gameplay.EnemySystem.Spawn;
using Leopotam.Ecs;
using System.Linq;

namespace Gameplay.Game.ECS.Features
{
    public class EnemyDieSystem : IEcsRunSystem
    {
        private readonly EnemySpawner enemySpawner = null;
        private readonly EcsFilter<TagEnemy, HealthComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var enemy = ref filter.Get1(i);
                ref var healthComponent = ref filter.Get2(i);

                if (healthComponent.Health > 0) continue;

                int enemyId = enemy.Id;
                var enemyController = enemySpawner.ActiveEnemies.First(x => x.Id == enemyId);
                enemySpawner.RemoveEnemy(enemyId);

                enemyController.Destroy();
                ref var enemyEntity = ref filter.GetEntity(i);
                enemyEntity.Destroy();
            }
        }
    }
}
