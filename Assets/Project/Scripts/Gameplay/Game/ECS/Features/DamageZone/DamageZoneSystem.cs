using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class DamageZoneSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DamageZone> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var zoneEntity = ref filter.GetEntity(i);
                ref var zone = ref filter.Get1(i);

                bool hasLayerData = zoneEntity.Has<DamageZoneTargetLayerData>();
                int targetLayer = hasLayerData ? zoneEntity.Get<DamageZoneTargetLayerData>().LayerMask : -1;

                var colliders = Physics.OverlapBox(zone.Center, zone.Border / 2, zone.Orientation, targetLayer);

                foreach (var collider in colliders)
                {
                    if(collider.TryGetComponent(out IEcsEntityHolder holder))
                    {
                        EventBus.Invoke(new ApplyDamageRequest()
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
