using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class AgentFollowSystem : IEcsRunSystem
    {
        private const float DestinationUpdateRate = 0.5f;
        private readonly EcsFilter<AgentMovableComponent, AgentFollowComponent> filter = null;

        public void Run()
        {
            float deltaTime = Time.deltaTime;

            foreach (var i in filter)
            {
                ref var agent = ref filter.Get1(i).NavMeshAgent;
                ref var follow = ref filter.Get2(i);

                follow.DestinationUpdateTime -= deltaTime;

                if (follow.DestinationUpdateTime <= 0)
                {
                    follow.DestinationUpdateTime = DestinationUpdateRate;
                    agent.SetDestination(follow.Target.position);
                }
            }
        }
    }
}
