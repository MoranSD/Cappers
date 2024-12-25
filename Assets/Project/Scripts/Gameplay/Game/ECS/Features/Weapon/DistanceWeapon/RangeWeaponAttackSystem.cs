using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class RangeWeaponAttackSystem : IEcsInitSystem, IEcsDestroySystem
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
            if (request.IsAbleToAttack == false) return;
            if (request.ExtensionData.ContainsKey(AttackRequest.TARGET_EXTENSION_DATA_KEY) == false) return;

            ref var weaponEntity = ref request.Sender;
            if (weaponEntity.Has<RangeWeaponTag>() == false) return;

            ref var weaponOwner = ref weaponEntity.Get<WeaponOwnerData>().Owner;
            var target = (EcsEntity)request.ExtensionData[AttackRequest.TARGET_EXTENSION_DATA_KEY];
            ref var attackDistance = ref weaponEntity.Get<WeaponAttackDistanceData>().AttackDistance;

            if (EntityUtil.GetDistance(weaponOwner, target) > attackDistance)
                return;

            if (weaponEntity.Has<AttackCoolDownComponent>())
            {
                ref var coolDownComponent = ref weaponEntity.Get<AttackCoolDownComponent>();
                coolDownComponent.AttackCoolDown += coolDownComponent.AttackRate;
            }

            ref var damage = ref weaponEntity.Get<WeaponDamageData>().Damage;

            EventBus.Invoke(new ApplyDamageRequest()
            {
                Sender = weaponOwner,
                Target = target,
                Damage = damage,
            });
        }
    }
}
