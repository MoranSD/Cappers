using Gameplay.Player.Data;
using Infrastructure.GameInput;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class PlayerAttackSystem : IEcsRunSystem
    {
        private readonly GameConfig gameConfig = null;
        private readonly PlayerConfigSO playerConfig = null;
        private readonly IInput input = null;
        private readonly EcsFilter<TagPlayer, TranslationComponent, TargetLookComponent, PlayerWeaponLink>.Exclude<BlockFreezed> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                if ((input.IsMeleeAttackButtonPressed || input.IsRangeAttackButtonPressed) == false) continue;

                ref var weaponsLink = ref filter.Get4(i);

                if (input.IsMeleeAttackButtonPressed && HasCoolDown(ref weaponsLink.MeleeWeapon)) continue;
                if (input.IsRangeAttackButtonPressed && HasCoolDown(ref weaponsLink.RangeWeapon)) continue;

                ref var transform = ref filter.Get2(i).Transform;
                ref var targetLook = ref filter.Get3(i);
                ref var playerEntity = ref filter.GetEntity(i);

                PerformAttack(ref weaponsLink, transform, ref targetLook, ref playerEntity);
            }
        }

        private bool HasCoolDown(ref EcsEntity weaponEntity)
        {
            ref var coolDown = ref weaponEntity.Get<AttackCoolDownComponent>();
            return coolDown.AttackCoolDown > 0;
        }
        private void PerformAttack(ref PlayerWeaponLink weaponsLink, Transform transform, ref TargetLookComponent targetLook, ref EcsEntity playerEntity)
        {
            bool isRange = input.IsRangeAttackButtonPressed;

            if (targetLook.HasTargetsInRange == false && isRange) return;

            bool isMelee = input.IsMeleeAttackButtonPressed;

            var closestTarget = targetLook.HasTargetsInRange ? 
                EntityUtil.GetClosestEntity(transform, targetLook.Targets) : 
                default;

            Vector3 attackDirection = transform.forward;

            if (targetLook.HasTargetsInRange)
            {
                attackDirection = EntityUtil.GetDirectionToEntity(transform, closestTarget);
                if (attackDirection != Vector3.zero) attackDirection.Normalize();
            }

            PerformAttackFeel(attackDirection, ref playerEntity, isMelee ? 0 : isRange ? 1 : -1);

            var targetWeapon = isMelee ? weaponsLink.MeleeWeapon : isRange ? weaponsLink.RangeWeapon : default;

            var attackRequest = new AttackRequest()
            {
                Sender = targetWeapon,
                ExtensionData = new()
                    {
                        { AttackRequest.TARGET_LAYER_EXTENSION_DATA_KEY, gameConfig.PlayerTargetLayers }
                    },
            };

            if (targetLook.HasTargetsInRange)
            {
                attackRequest.ExtensionData.Add(AttackRequest.TARGET_EXTENSION_DATA_KEY, closestTarget);
                //look at closestTarget
            }

            EventBus.Invoke(attackRequest);

        }
        private void PerformAttackFeel(Vector3 attackDirection, ref EcsEntity playerEntity, int attackType)
        {
            float slowedSpeed = playerConfig.MainConfig.FightConfig.SlowedMoveSpeed;

            //0 - melee
            //1 - range
            float slowDownDuration = attackType == 0 ? playerConfig.MainConfig.FightConfig.MeleeMoveSlowDownDuration :
                attackType == 1 ? playerConfig.MainConfig.FightConfig.LongMoveSlowDownDuration : 0;

            //_world.NewEntityWithComponent<ApplyVelocityEvent>(new()
            //{
            //    Target = playerEntity,
            //    Direction = attackDirection,
            //    Force = playerConfig.MainConfig.FightConfig.MeleePushForce,
            //    IsTemporary = true,
            //    Duration = playerConfig.MainConfig.FightConfig.PushForceDuration,
            //});

            //TODO: Look at target if range
            /*
             * если стреляем, то игрок должен посмотреть моментально но плавно на цель
             */

            EventBus.Invoke(new ApplySlowDownRequest()
            {
                Target = playerEntity,
                Duration = slowDownDuration,
                SlowSpeed = slowedSpeed,
                WithSmoothRecovery = true,
            });
        }
    }
}
