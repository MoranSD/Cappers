using Gameplay.Ship.UnitControl.Placement;
using Leopotam.Ecs;
using System.Linq;

namespace Gameplay.Game.ECS.Features
{
    public class UnitGoToIdleAfterFightSystem : IEcsRunSystem
    {
        private readonly ShipUnitPlacement unitPlacement = null;
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

                var requestEntity = _world.NewEntity();
                ref var entity = ref filter.GetEntity(i);
                ref var destinationRequest = ref requestEntity.Get<AgentSetDestinationRequest>();

                destinationRequest.Target = entity;
                int unitId = filter.Get3(i).Id;
                destinationRequest.Destination = unitPlacement.GetUnitIdlePosition(unitId);
            }
        }
    }
}
