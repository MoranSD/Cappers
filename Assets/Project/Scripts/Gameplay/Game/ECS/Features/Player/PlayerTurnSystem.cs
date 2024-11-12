using Gameplay.Player;
using Leopotam.Ecs;
using System.Linq;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class PlayerTurnSystem : IEcsRunSystem
    {
        private readonly PlayerController playerController = null;
        private readonly EcsFilter<TranslationComponent, TFTurnableComponent, ChMovableComponent, TagPlayer> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var translation = ref filter.Get1(i);
                ref var turnable = ref filter.Get2(i);
                ref var movable = ref filter.Get3(i);
                ref var transform = ref translation.Transform;

                if (TryGetTargetsAround(transform, out var targets))
                {
                    var closestTarget = GetClosestTarget(transform.position, targets);
                    var directionToTarget = closestTarget.GetPosition() - transform.position;
                    directionToTarget.y = 0;

                    turnable.Direction = directionToTarget;
                    turnable.Speed = playerController.Config.MovementConfig.TargetLookSpeed;
                }
                else
                {
                    turnable.Direction = movable.Direction;
                    turnable.Speed = playerController.Config.MovementConfig.LookSpeed;
                }
            }
        }

        private bool TryGetTargetsAround(Transform transform, out IAttackTarget[] targets)
        {
            var lookRange = playerController.Config.FightConfig.AttackRange;
            var enemyLayer = playerController.EnemyLayer;
            var colliders = Physics.OverlapSphere(transform.position, lookRange, enemyLayer);

            targets = colliders
                .Where(x => x.TryGetComponent(out IAttackTargetView attackTargetView) && attackTargetView.Target.IsDead == false)
                .Select(x => x.GetComponent<IAttackTargetView>().Target)
                .ToArray();

            return targets.Length > 0;
        }

        private IAttackTarget GetClosestTarget(Vector3 position, IAttackTarget[] targets)
        {
            var closestTarget = targets[0];

            foreach (var target in targets)
            {
                if (Vector3.Distance(position, target.GetPosition()) <
                    Vector3.Distance(position, closestTarget.GetPosition()))
                    closestTarget = target;
            }

            return closestTarget;
        }
    }
}
