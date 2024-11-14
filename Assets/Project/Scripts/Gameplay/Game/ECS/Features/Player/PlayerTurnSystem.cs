using Gameplay.Player.Data;
using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class PlayerTurnSystem : IEcsRunSystem
    {
        private readonly PlayerConfigSO playerConfig = null;
        private readonly EcsFilter<TranslationComponent, TFTurnableComponent, ChMovableComponent, TagPlayer>.Exclude<BlockFreezed> filter = null;

        public void Run()
        {
            float attackRange = playerConfig.MainConfig.FightConfig.AttackRange;
            float targetLookSpeed = playerConfig.MainConfig.MovementConfig.TargetLookSpeed;
            float lookSpeed = playerConfig.MainConfig.MovementConfig.LookSpeed;

            foreach (var i in filter)
            {
                ref var translation = ref filter.Get1(i);
                ref var turnable = ref filter.Get2(i);
                ref var movable = ref filter.Get3(i);
                ref var transform = ref translation.Transform;

                if (EnvironmentProvider.TryGetEnemyHoldersAround(transform, attackRange, out var enemyHolders))
                {
                    var closestTargetHolder = EnvironmentProvider.GetClosestHolder(transform.position, enemyHolders);
                    var directionToTarget = closestTargetHolder.transform.position - transform.position;
                    directionToTarget.y = 0;

                    turnable.Direction = directionToTarget;
                    turnable.Speed = targetLookSpeed;
                }
                else
                {
                    turnable.Direction = movable.Direction;
                    turnable.Speed = lookSpeed;
                }
            }
        }
    }
}
