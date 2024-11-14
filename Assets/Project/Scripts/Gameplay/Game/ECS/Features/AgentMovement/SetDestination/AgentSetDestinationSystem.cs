using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class AgentSetDestinationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AgentSetDestinationRequest> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var setDestinationEvent = ref filter.Get1(i);
                ref var agent = ref setDestinationEvent.Target.Get<AgentMovableComponent>().NavMeshAgent;

                agent.SetDestination(setDestinationEvent.Destination);
            }
        }
    }
}
