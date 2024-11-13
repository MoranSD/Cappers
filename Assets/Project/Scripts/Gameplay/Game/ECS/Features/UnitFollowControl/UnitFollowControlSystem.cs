using Gameplay.Ship.UnitControl.Placement;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitFollowControlSystem : IEcsRunSystem
    {
        private readonly ShipUnitPlacement placement = null;
        private readonly EcsFilter<TranslationComponent, UnitFollowControlEvent, UnitFollowControlComponent> filter = null;
        public void Run()
        {
            foreach (var i in filter)
            {
                ref var transform = ref filter.Get1(i).Transform;
                ref var followControlEvent = ref filter.Get2(i);
                ref var unitControl = ref filter.Get3(i);

                if(EnvironmentProvider.TryGetUnitAround(transform, followControlEvent.Range, out EcsEntity unit))
                {
                    if (unitControl.UnitsInControl.Contains(unit))
                    {
                        unitControl.UnitsInControl.Remove(unit);
                        unit.Del<AgentFollowComponent>();

                        if (unit.Has<TagUnit>())
                        {
                            ref var unitTag = ref unit.Get<TagUnit>();
                            ref var destinationEvent = ref unit.Get<AgentSetDestinationEvent>();
                            destinationEvent.Destination = placement.GetUnitIdlePosition(unitTag.Id);
                        }
                        else
                        {
                            Debug.Log("No unit tag");
                        }
                    }
                    else
                    {
                        unitControl.UnitsInControl.Add(unit);
                        ref var followComponent = ref unit.Get<AgentFollowComponent>();
                        followComponent.Target = transform;
                    }
                }
            }
        }
    }
}
