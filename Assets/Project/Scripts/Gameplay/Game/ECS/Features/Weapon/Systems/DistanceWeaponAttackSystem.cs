using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class DistanceWeaponAttackSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<WeaponAttackRequest> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var request = ref filter.Get1(i);
                ref var weaponEntity = ref request.WeaponSender;

                if(weaponEntity.Has<DistanceWeaponComponent>() == false)
                    continue;

                ref var distanceWeapon = ref weaponEntity.Get<DistanceWeaponComponent>();
                ref var weaponOwner = ref weaponEntity.Get<WeaponOwnerComponent>().Owner;

                if (EntityUtil.GetDistance(weaponOwner, request.Target) > distanceWeapon.AttackDistance)
                    continue;

                if (weaponEntity.Has<WeaponCoolDownComponent>())
                {
                    ref var coolDownComponent = ref weaponEntity.Get<WeaponCoolDownComponent>();
                    coolDownComponent.AttackCoolDown += coolDownComponent.AttackRate;
                }

                _world.NewEntityWithComponent<ApplyDamageRequest>(new()
                {
                    Sender = weaponOwner,
                    Target = request.Target,
                    Damage = distanceWeapon.Damage,
                });
            }
        }
    }
}
