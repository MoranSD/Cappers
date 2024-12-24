using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class DistanceWeaponAttackSystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Init()
        {
            EventBus.Subscribe<AttackRequest>(OnWeaponAttack);
        }

        public void Destroy()
        {
            EventBus.Unsubscribe<AttackRequest>(OnWeaponAttack);
        }

        private void OnWeaponAttack(AttackRequest request)
        {
            if (request.HasCooldown) return;

            if (request.ExtensionData.ContainsKey(AttackRequest.TARGET_EXTENSION_DATA_KEY) == false) return;

            var target = (EcsEntity)request.ExtensionData[AttackRequest.TARGET_EXTENSION_DATA_KEY];
            ref var weaponEntity = ref request.Sender;

            ref var distanceWeapon = ref weaponEntity.Get<DistanceWeaponComponent>();
            ref var weaponOwner = ref weaponEntity.Get<WeaponOwnerComponent>().Owner;

            if (EntityUtil.GetDistance(weaponOwner, target) > distanceWeapon.AttackDistance)
                return;

            if (weaponEntity.Has<AttackCoolDownComponent>())
            {
                ref var coolDownComponent = ref weaponEntity.Get<AttackCoolDownComponent>();
                coolDownComponent.AttackCoolDown += coolDownComponent.AttackRate;
            }

            EventBus.Invoke(new ApplyDamageRequest()
            {
                Sender = weaponOwner,
                Target = target,
                Damage = distanceWeapon.Damage,
            });
        }
    }
}
