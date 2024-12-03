using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class EnemyDieSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TagEnemy, HealthComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var enemy = ref filter.Get1(i);
                ref var healthComponent = ref filter.Get2(i);

                if (healthComponent.Health > 0) continue;

                enemy.Controller.Destroy();
                ref var enemyEntity = ref filter.GetEntity(i);
                enemyEntity.Destroy();
            }
        }
    }
}
