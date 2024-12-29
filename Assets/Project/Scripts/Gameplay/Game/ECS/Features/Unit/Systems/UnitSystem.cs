using Gameplay.Ship.UnitControl;
using Gameplay.UnitSystem;
using Gameplay.UnitSystem.Upgrade;
using Leopotam.Ecs;
using System;
using Utils;
using Voody.UniLeo;

namespace Gameplay.Game.ECS.Features
{
    public class UnitSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly GameState gameState = null;
        private readonly ShipUnitExistenceControl existenceControl = null;

        public void Destroy()
        {
            EventBus.Unsubscribe<UnitHealedEvent>(OnHeal);
            EventBus.Unsubscribe<UnitUpgradedEvent>(OnUgrade);
            EventBus.Unsubscribe<UnitBeginInteractEvent>(OnBeginInteract);
            EventBus.Unsubscribe<UnitEndInteractEvent>(OnEndInteract);
            EventBus.Unsubscribe<UnitEndInteractJobEvent>(OnEndInteractJob);
            EventBus.Unsubscribe<ApplyDamageEvent>(OnApplyDamage);
            EventBus.Unsubscribe<RemovedFollowControlEvent>(OnRemoveFollow);
            EventBus.Unsubscribe<AddedFollowControlEvent>(OnAddFollow);
            EventBus.Unsubscribe<EndAgroEvent>(OnEndAgro);
            EventBus.Unsubscribe<BeginAgroEvent>(OnBeginAgro);
        }

        public void Init()
        {
            EventBus.Subscribe<UnitHealedEvent>(OnHeal);
            EventBus.Subscribe<UnitUpgradedEvent>(OnUgrade);
            EventBus.Subscribe<UnitBeginInteractEvent>(OnBeginInteract);
            EventBus.Subscribe<UnitEndInteractEvent>(OnEndInteract);
            EventBus.Subscribe<UnitEndInteractJobEvent>(OnEndInteractJob);
            EventBus.Subscribe<ApplyDamageEvent>(OnApplyDamage);
            EventBus.Subscribe<RemovedFollowControlEvent>(OnRemoveFollow);
            EventBus.Subscribe<AddedFollowControlEvent>(OnAddFollow);
            EventBus.Subscribe<EndAgroEvent>(OnEndAgro, 1);
            EventBus.Subscribe<BeginAgroEvent>(OnBeginAgro);
        }

        private void OnHeal(UnitHealedEvent healEvent)
        {
            var unitData = gameState.GetUnitDataById(healEvent.UnitId);
            var filter = WorldHandler.GetWorld().GetFilter(typeof(EcsFilter<TagUnit>));

            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                ref var tag = ref entity.Get<TagUnit>();

                if (tag.Controller.Id != healEvent.UnitId) continue;

                ref var health = ref entity.Get<HealthComponent>();
                health.Health = unitData.MaxHealth;

                break;
            }
        }
        private void OnUgrade(UnitUpgradedEvent upgradeEvent)
        {
            var unitData = gameState.GetUnitDataById(upgradeEvent.UnitId);
            var filter = WorldHandler.GetWorld().GetFilter(typeof(EcsFilter<TagUnit>));

            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                ref var tag = ref entity.Get<TagUnit>();

                if (tag.Controller.Id != unitData.Id) continue;

                ref var health = ref entity.Get<HealthComponent>();
                health.Health = unitData.MaxHealth;

                tag.Controller.NavMeshAgent.speed = unitData.Speed;

                ref var weaponLink = ref entity.Get<WeaponLink>();
                ref var damage = ref weaponLink.Weapon.Get<WeaponDamageData>();
                damage.Damage = unitData.Damage;

                break;
            }
        }

        private void OnBeginAgro(BeginAgroEvent beginsAgroEvent)
        {
            ref var entity = ref beginsAgroEvent.Entity;

            if (entity.Has<TagUnit>() == false)
                return;

            entity.Get<BlockFollowControl>();
            entity.Get<BlockUnitInteractJob>();
        }
        private void OnEndAgro(EndAgroEvent beginsAgroEvent)
        {
            ref var entity = ref beginsAgroEvent.Entity;

            if (entity.Has<TagUnit>() == false)
                return;

            entity.Del<BlockFollowControl>();
            entity.Del<BlockUnitInteractJob>();

            if (entity.Has<TagUnderFollowControl>() || entity.Has<UnitInteractJobComponent>())
                return;

            ref var unitTag = ref entity.Get<TagUnit>();
            unitTag.Controller.GoToIdlePosition();
        }


        private void OnAddFollow(AddedFollowControlEvent addEvent)
        {
            ref var entity = ref addEvent.Target;

            if (entity.Has<TagUnit>() == false)
                return;

            if (entity.Has<UnitInteractJobComponent>() == false)
                return;

            EventBus.Invoke<UnitCancelInteractJobRequest>(new()
            {
                Target = entity,
            });
        }
        private void OnRemoveFollow(RemovedFollowControlEvent removeEvent)
        {
            ref var entity = ref removeEvent.Target;

            if (entity.Has<TagUnit>() == false)
                return;

            ref var unitTag = ref entity.Get<TagUnit>();
            unitTag.Controller.GoToIdlePosition();
        }

        private void OnApplyDamage(ApplyDamageEvent damageEvent)
        {
            if (damageEvent.Target.Has<TagUnit>() == false) return;
            if (damageEvent.Target.Has<HealthComponent>() == false) return;

            ref var unit = ref damageEvent.Target.Get<TagUnit>();
            ref var health = ref damageEvent.Target.Get<HealthComponent>();

            int unitId = unit.Controller.Id;
            float currentHealth = health.Health;

            gameState.ChangeUnitData(unitId, (d) =>
            {
                d.CurrentHealth = MathF.Min(0, currentHealth);
                return d;
            });

            if (health.Health <= 0)
            {
                EventBus.Invoke<UnitDieEvent>(new()
                {
                    UnitId = unit.Controller.Id
                });

                existenceControl.RemoveUnit(unitId);

                unit.Controller.Destroy();
                ref var weapon = ref damageEvent.Target.Get<WeaponLink>().Weapon;
                weapon.Destroy();
                damageEvent.Target.Destroy();
            }
        }

        private void OnEndInteractJob(UnitEndInteractJobEvent interactEvent)
        {
            ref var entity = ref interactEvent.Entity;
            ref var unit = ref entity.Get<TagUnit>();

            if (unit.Controller.IsInteracting) return;
            if (entity.Has<TagUnderFollowControl>()) return;

            unit.Controller.GoToIdlePosition();
        }
        private void OnBeginInteract(UnitBeginInteractEvent interactEvent)
        {
            ref var entity = ref interactEvent.Entity;

            entity.Get<BlockAgro>();
            entity.Get<BlockFollowControl>();
            entity.Get<BlockFreezed>();
            entity.Get<BlockUnitInteractJob>();
        }
        private void OnEndInteract(UnitEndInteractEvent interactEvent)
        {
            ref var entity = ref interactEvent.Entity;

            entity.Del<BlockAgro>();
            entity.Del<BlockFollowControl>();
            entity.Del<BlockFreezed>();
            entity.Del<BlockUnitInteractJob>();

            if (entity.Has<TagUnderFollowControl>()) return;

            ref var unit = ref entity.Get<TagUnit>();
            unit.Controller.GoToIdlePosition();
        }
    }
}
