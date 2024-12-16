using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitApplyInteractJobSystem : IEcsRunSystem
    {
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<UnitInteractJobRequest> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var request = ref filter.Get1(i);

                if (request.Target.Has<TagUnit>() == false) continue;

                ref var unit = ref request.Target.Get<TagUnit>().Controller;

                if(Vector3.Distance(unit.transform.position, request.Interactable.Position) <= GameConstants.UnitInteractDistance)
                {
                    request.Interactable.Interact(unit);
                }
                else
                {
                    _world.NewOneFrameEntity(new AgentSetDestinationRequest()
                    {
                        Target = request.Target,
                        Destination = request.Interactable.Position,
                    });

                    ref var job = ref request.Target.Get<UnitInteractJob>();
                    job.Interactable = request.Interactable;
                }
            }
        }
    }
}
