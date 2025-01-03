﻿using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class MeleeWeaponAttackSystem : IEcsDestroySystem, IEcsInitSystem
    {
        private readonly EcsWorld _world = null;

        public void Destroy()
        {
            EventBus.Unsubscribe<AttackRequest>(OnWeaponAttack);
        }

        public void Init()
        {
            EventBus.Subscribe<AttackRequest>(OnWeaponAttack);
        }

        private void OnWeaponAttack(AttackRequest request)
        {
            if (request.IsAbleToAttack == false) return;
            if (request.ExtensionData.ContainsKey(AttackRequest.TARGET_LAYER_EXTENSION_DATA_KEY) == false) return;

            ref var weaponEntity = ref request.Sender;
            if (weaponEntity.Has<MeleeWeaponTag>() == false) return;

            if (weaponEntity.Has<AttackCoolDownComponent>())
            {
                ref var coolDownComponent = ref weaponEntity.Get<AttackCoolDownComponent>();
                coolDownComponent.AttackCoolDown += coolDownComponent.AttackRate;
            }

            ref var damage = ref weaponEntity.Get<WeaponDamageData>().Damage;
            ref var meleeWeaponData = ref weaponEntity.Get<MeleeWeaponData>();

            var zoneEntity = _world.NewEntity()
                .Replace(new DamageZone()
                {
                    Center = meleeWeaponData.ZonePivot.position,
                    Border = meleeWeaponData.ZoneBorders,
                    Orientation = meleeWeaponData.ZonePivot.rotation,
                    Damage = damage,
                })
                .Replace(new DamageZoneTargetLayerData()
                {
                    LayerMask = (LayerMask)request.ExtensionData[AttackRequest.TARGET_LAYER_EXTENSION_DATA_KEY],
                })
                .Replace(new OneFrameEntity());
        }
    }
}
