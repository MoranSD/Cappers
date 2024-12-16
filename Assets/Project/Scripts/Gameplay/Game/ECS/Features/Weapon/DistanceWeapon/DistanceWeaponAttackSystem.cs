using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class DistanceWeaponAttackSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<AttackRequest, AttackRequestTargetData> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var request = ref filter.Get1(i);
                ref var target = ref filter.Get2(i).Target;

                ref var weaponEntity = ref request.WeaponSender;

                if (weaponEntity.Has<DistanceWeaponComponent>() == false) continue;

                ref var distanceWeapon = ref weaponEntity.Get<DistanceWeaponComponent>();
                ref var weaponOwner = ref weaponEntity.Get<WeaponOwnerComponent>().Owner;

                if (EntityUtil.GetDistance(weaponOwner, target) > distanceWeapon.AttackDistance)
                    continue;

                if (weaponEntity.Has<AttackCoolDownComponent>())
                {
                    ref var coolDownComponent = ref weaponEntity.Get<AttackCoolDownComponent>();
                    coolDownComponent.AttackCoolDown += coolDownComponent.AttackRate;
                }

                _world.NewOneFrameEntity(new ApplyDamageRequest()
                {
                    Sender = weaponOwner,
                    Target = target,
                    Damage = distanceWeapon.Damage,
                });
            }
        }
    }
}
