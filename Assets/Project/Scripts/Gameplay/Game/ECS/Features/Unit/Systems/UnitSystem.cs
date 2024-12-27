using Gameplay.Ship.UnitControl;
using Gameplay.UnitSystem;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitSystem : IEcsInitSystem, IEcsDestroySystem
    {
        private readonly ShipUnitExistenceControl existenceControl = null;

        public void Destroy()
        {
            EventBus.Unsubscribe<UnitBeginInteractEvent>(OnBeginInteract);
            EventBus.Unsubscribe<UnitEndInteractEvent>(OnEndInteract);
            EventBus.Unsubscribe<UnitInteractJobRequest>(OnJob);
            EventBus.Unsubscribe<UnitEndInteractJobEvent>(OnEndInteractJob);
            EventBus.Unsubscribe<ApplyDamageEvent>(OnApplyDamage);
            EventBus.Unsubscribe<RemovedFollowControlEvent>(OnRemoveFollow);
            EventBus.Unsubscribe<EndAgroEvent>(OnEndAgro);
            EventBus.Unsubscribe<BeginAgroEvent>(OnBeginAgro);
        }

        public void Init()
        {
            EventBus.Subscribe<UnitBeginInteractEvent>(OnBeginInteract);
            EventBus.Subscribe<UnitEndInteractEvent>(OnEndInteract);
            EventBus.Subscribe<UnitInteractJobRequest>(OnJob);
            EventBus.Subscribe<UnitEndInteractJobEvent>(OnEndInteractJob);
            EventBus.Subscribe<ApplyDamageEvent>(OnApplyDamage);
            EventBus.Subscribe<RemovedFollowControlEvent>(OnRemoveFollow);
            EventBus.Subscribe<EndAgroEvent>(OnEndAgro, 1);
            EventBus.Subscribe<BeginAgroEvent>(OnBeginAgro);
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

            if (entity.Has<TagUnderFollowControl>())
                return;

            ref var unitTag = ref entity.Get<TagUnit>();
            unitTag.Controller.GoToIdlePosition();
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

            if (health.Health <= 0)
            {
                EventBus.Invoke<UnitDieEvent>(new()
                {
                    UnitId = unit.Controller.Id
                });

                int unitId = unit.Controller.Id;
                existenceControl.RemoveUnit(unitId);

                unit.Controller.Destroy();
                ref var weapon = ref damageEvent.Target.Get<WeaponLink>().Weapon;
                weapon.Destroy();
                damageEvent.Target.Destroy();
            }
        }

        private void OnJob(UnitInteractJobRequest request)
        {
            if (request.Target.Has<TagUnit>() == false) return;

            ref var unit = ref request.Target.Get<TagUnit>().Controller;

            ref var follow = ref request.Target.Get<FollowComponent>();
            follow.Target = request.Interactable.Pivot;

            ref var job = ref request.Target.Get<UnitInteractJobComponent>();
            job.Interactable = request.Interactable;
        }
        private void OnEndInteractJob(UnitEndInteractJobEvent interactEvent)
        {
            ref var entity = ref interactEvent.Entity;
            ref var unit = ref entity.Get<TagUnit>();

            if (unit.Controller.IsInteracting) return;

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

            ref var unit = ref entity.Get<TagUnit>();
            unit.Controller.GoToIdlePosition();
        }
    }
}
