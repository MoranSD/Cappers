using Gameplay.Player.Data;
using Infrastructure.GameInput;
using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private readonly PlayerConfigSO playerConfig = null;
        private readonly IInput input = null;
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TagPlayer, TranslationComponent, TargetLookComponent> filter = null;
        public void Run()
        {
            if (input.MeleeAttackButtonPressed)
            {
                var meleeAttackDistance = playerConfig.MainConfig.FightConfig.MeleeAttackDistance;
                var meleeDamage = playerConfig.MainConfig.FightConfig.BaseMeleeDamage;
                PerformAttacks(meleeAttackDistance, meleeDamage);
            }

            if (input.RangeAttackButtonPressed)
            {
                var rangeAttackDistance = playerConfig.MainConfig.FightConfig.LongAttackDistance;
                var rangeDamage = playerConfig.MainConfig.FightConfig.BaseLongDamage;
                PerformAttacks(rangeAttackDistance, rangeDamage);
            }
        }

        private void PerformAttacks(float attackDistance, float damage)
        {
            foreach (var i in filter)
            {
                ref var playerEntity = ref filter.GetEntity(i);
                ref var transform = ref filter.Get2(i).Transform;
                ref var targetLook = ref filter.Get3(i);

                if (targetLook.HasTargetsInRange)
                {
                    var closestTarget = EntityUtil.GetClosestEntity(transform, targetLook.Targets);

                    if (EntityUtil.GetDistance(transform, closestTarget) > attackDistance)
                        continue;

                    _world.NewEntityWithComponent<ApplyDamageRequest>(new()
                    {
                        Sender = playerEntity,
                        Target = closestTarget,
                        Damage = damage,
                    });
                }
            }
        }
    }
}
