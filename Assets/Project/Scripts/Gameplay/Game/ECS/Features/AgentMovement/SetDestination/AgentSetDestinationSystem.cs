using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class AgentSetDestinationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AgentMovableComponent, AgentSetDestinationEvent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var agent = ref filter.Get1(i).NavMeshAgent;
                ref var setDestinationEvent = ref filter.Get2(i);

                agent.SetDestination(setDestinationEvent.Destination);
            }
        }
    }
}
