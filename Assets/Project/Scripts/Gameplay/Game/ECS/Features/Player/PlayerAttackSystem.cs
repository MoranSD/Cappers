using Infrastructure.GameInput;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private readonly IInput input = null;
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TagPlayer, TranslationComponent, TargetLookComponent, PlayerWeaponLink>.Exclude<BlockFreezed> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var transform = ref filter.Get2(i).Transform;
                ref var targetLook = ref filter.Get3(i);
                ref var weaponsLink = ref filter.Get4(i);

                if (targetLook.HasTargetsInRange == false)
                    continue;

                if (input.MeleeAttackButtonPressed)
                    PerformAttack(transform, ref targetLook.Targets, ref weaponsLink.MeleeWeapon);

                if (input.RangeAttackButtonPressed)
                    PerformAttack(transform, ref targetLook.Targets, ref weaponsLink.RangeWeapon);
            }
        }
        private void PerformAttack(Transform transform, ref EcsEntity[] targets, ref EcsEntity weapon)
        {
            var closestTarget = EntityUtil.GetClosestEntity(transform, targets);

            _world.NewEntityWithComponent<AttackRequest>(new()
            {
                Sender = weapon,
                Target = closestTarget,
            });
        }
    }
}
