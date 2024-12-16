using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class MeleeWeaponAttackSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<AttackRequest, AttackRequestTargetLayerData> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var request = ref filter.Get1(i);
                ref var weaponEntity = ref request.WeaponSender;

                if (weaponEntity.Has<MeleeWeaponComponent>() == false) continue;

                if (weaponEntity.Has<AttackCoolDownComponent>())
                {
                    ref var coolDownComponent = ref weaponEntity.Get<AttackCoolDownComponent>();
                    coolDownComponent.AttackCoolDown += coolDownComponent.AttackRate;
                }

                ref var meleeWeapon = ref weaponEntity.Get<MeleeWeaponComponent>();

                ref var requestLayerData = ref filter.Get2(i);
                var zoneEntity = _world.NewEntity()
                    .Replace(new DamageZone()
                    {
                        Center = meleeWeapon.ZonePivot.position,
                        Border = meleeWeapon.ZoneBorders,
                        Orientation = meleeWeapon.ZonePivot.rotation,
                        Damage = meleeWeapon.Damage,
                    })
                    .Replace(new DamageZoneTargetLayerData()
                    {
                        LayerMask = requestLayerData.LayerMask,
                    })
                    .Replace(new OneFrameEntity());
            }
        }
    }
}
