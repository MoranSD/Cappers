using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class DamageZoneSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<OneFrameDamageZone> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var zoneEntity = ref filter.GetEntity(i);
                ref var zone = ref filter.Get1(i);

                var colliders = Physics.OverlapBox(zone.Center, zone.Border / 2, zone.Orientation);

                foreach (var collider in colliders)
                {
                    if(collider.TryGetComponent(out IEcsEntityHolder holder))
                    {
                        _world.NewEntityWithComponent<ApplyDamageRequest>(new()
                        {
                            Damage = zone.Damage,
                            Sender = zoneEntity,
                            Target = holder.EcsEntity
                        });
                    }
                }
            }
        }
    }
}
