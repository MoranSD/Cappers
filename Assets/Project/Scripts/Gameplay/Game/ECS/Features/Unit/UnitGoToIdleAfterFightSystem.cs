using Gameplay.Ship.UnitControl;
using Leopotam.Ecs;
using System.Linq;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitGoToIdleAfterFightSystem : IEcsRunSystem
    {
        private readonly ShipUnitExistenceControl unitExistence = null;
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TargetAgroComponent, TargetLookComponent, TagUnit>.Exclude<TagUnderFollowControl> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var agro = ref filter.Get1(i);

                /*
                 * if unit now or was in fight, he should have agro.HasTarget as true
                 * than his target should be dead (IsAlive() == false)
                 * 
                 * so, if he has no targets alive he comes back to player (if he is under player control) 
                 * or goes to his idle position
                 */

                if (agro.HasTarget == false) continue;
                if (agro.Target.IsAlive()) continue;
                ref var look = ref filter.Get2(i);
                if (look.Targets.Any(x => x.IsAlive())) continue;

                ref var entity = ref filter.GetEntity(i);
                int unitId = filter.Get3(i).Id;

                _world.NewEntityWithComponent<AgentSetDestinationRequest>(new()
                {
                    Target = entity,
                    Destination = unitExistence.GetUnitIdlePosition(unitId)
                });
            }
        }
    }
}
