using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class AgentFollowSystem : IEcsRunSystem
    {
        private const float DestinationUpdateRate = 0.5f;
        private readonly EcsFilter<AgentMovableComponent, FollowComponent, AgentDestinationUpdateTimeData>.Exclude<BlockFreezed> filter = null;

        public void Run()
        {
            float deltaTime = Time.deltaTime;

            foreach (var i in filter)
            {
                ref var followTarget = ref filter.Get2(i);

                //if (followTarget.Target == null)
                //    continue;

                TrySetDestination(ref filter.GetEntity(i), deltaTime, followTarget.Target.position);
            }
        }

        private void TrySetDestination(ref EcsEntity entity, float deltaTime, Vector3 destination)
        {
            ref var agent = ref entity.Get<AgentMovableComponent>().NavMeshAgent;
            ref var destimationTime = ref entity.Get<AgentDestinationUpdateTimeData>();

            destimationTime.DestinationUpdateTime -= deltaTime;

            if (destimationTime.DestinationUpdateTime <= 0)
            {
                destimationTime.DestinationUpdateTime = DestinationUpdateRate;

                agent.SetDestination(destination);
            }
        }
    }
}
