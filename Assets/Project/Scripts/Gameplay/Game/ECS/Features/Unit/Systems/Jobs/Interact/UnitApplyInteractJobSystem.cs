using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class UnitApplyInteractJobSystem : IEcsInitSystem, IEcsDestroySystem
    {
        public void Destroy()
        {
            EventBus.Unsubscribe<UnitInteractJobRequest>(OnJob);
        }

        public void Init()
        {
            EventBus.Subscribe<UnitInteractJobRequest>(OnJob);
        }

        private void OnJob(UnitInteractJobRequest request)
        {
            if (request.Target.Has<TagUnit>() == false) return;

            ref var unit = ref request.Target.Get<TagUnit>().Controller;

            if (Vector3.Distance(unit.transform.position, request.Interactable.Position) <= GameConstants.UnitInteractDistance)
            {
                request.Interactable.Interact(unit);
            }
            else
            {
                EventBus.Invoke(new AgentSetDestinationRequest()
                {
                    Target = request.Target,
                    Destination = request.Interactable.Position,
                });

                ref var job = ref request.Target.Get<UnitInteractJobComponent>();
                job.Interactable = request.Interactable;
            }
        }
    }
}
