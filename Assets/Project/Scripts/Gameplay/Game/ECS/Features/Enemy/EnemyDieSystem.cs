using Gameplay.EnemySystem.Factory;
using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class EnemyDieSystem : IEcsRunSystem
    {
        private readonly EnemyFactory factory = null;
        private readonly EcsFilter<TagEnemy, HealthComponent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var healthComponent = ref filter.Get2(i);

                if (healthComponent.Health > 0) continue;

                ref var enemy = ref filter.Get1(i);

                factory.RemoveDeadEnemy(enemy.Controller.Id);
                enemy.Controller.Destroy();
                ref var enemyEntity = ref filter.GetEntity(i);
                ref var weapon = ref enemyEntity.Get<WeaponLink>().Weapon;
                weapon.Destroy();
                enemyEntity.Destroy();
            }
        }
    }
}
