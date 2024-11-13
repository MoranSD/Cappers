using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitFollowControlSystem : IEcsRunSystem
    {
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
                        //stop follow
                    }
                    else
                    {
                        unitControl.UnitsInControl.Add(unit);
                        //begin follow
                    }
                }
            }
        }
    }
}
