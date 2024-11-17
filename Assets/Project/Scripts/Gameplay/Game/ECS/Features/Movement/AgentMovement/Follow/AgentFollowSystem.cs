using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public class AgentFollowSystem : IEcsRunSystem
    {
        private const float DestinationUpdateRate = 0.5f;
        private readonly EcsFilter<AgentMovableComponent, FollowComponent, AgentDestinationUpdateTime> filter = null;

        public void Run()
        {
            float deltaTime = Time.deltaTime;

            foreach (var i in filter)
            {
                ref var followTarget = ref filter.Get2(i);

                if (followTarget.Target.IsAlive() == false)
                {
                    ref var entity = ref filter.GetEntity(i);
                    entity.Del<FollowComponent>();
                    continue;
                }

                ref var agent = ref filter.Get1(i).NavMeshAgent;
                ref var destimationTime = ref filter.Get3(i);

                destimationTime.DestinationUpdateTime -= deltaTime;

                if (destimationTime.DestinationUpdateTime <= 0)
                {
                    destimationTime.DestinationUpdateTime = DestinationUpdateRate;

                    ref var targetTF = ref followTarget.Target.Get<TranslationComponent>().Transform;
                    agent.SetDestination(targetTF.position);
                }
            }
        }
    }
}
