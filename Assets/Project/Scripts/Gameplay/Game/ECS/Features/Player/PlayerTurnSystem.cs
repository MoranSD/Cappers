using Gameplay.Player.Data;
using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class PlayerTurnSystem : IEcsRunSystem
    {
        private readonly PlayerConfigSO playerConfig = null;
        private readonly EcsFilter<TranslationComponent, TFTurnableComponent, ChMovableComponent, TagPlayer, TargetLookComponent>.Exclude<BlockFreezed> filter = null;

        public void Run()
        {
            float targetLookSpeed = playerConfig.MainConfig.MovementConfig.TargetLookSpeed;
            float lookSpeed = playerConfig.MainConfig.MovementConfig.LookSpeed;

            foreach (var i in filter)
            {
                ref var transform = ref filter.Get1(i).Transform;
                ref var turnable = ref filter.Get2(i);
                ref var movable = ref filter.Get3(i);
                ref var targetLook = ref filter.Get5(i);

                if (targetLook.HasTargetsInRange)
                {
                    var closestTarget = EntityUtil.GetClosestEntity(transform, targetLook.Targets);
                    ref var closestTargetTF = ref closestTarget.Get<TranslationComponent>().Transform;
                    var directionToTarget = closestTargetTF.position - transform.position;
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
