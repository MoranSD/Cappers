using Leopotam.Ecs;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class AgentSetDestinationSystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Destroy()
        {
            EventBus.Unsubscribe<AgentSetDestinationRequest>(OnSetDestination);
        }

        public void Init()
        {
            EventBus.Subscribe<AgentSetDestinationRequest>(OnSetDestination);
        }

        private void OnSetDestination(AgentSetDestinationRequest setDestinationRequest)
        {
            if (setDestinationRequest.Target.Has<AgentMovableComponent>() == false) return;

            ref var agent = ref setDestinationRequest.Target.Get<AgentMovableComponent>().NavMeshAgent;

            agent.SetDestination(setDestinationRequest.Destination);
        }
    }
}
