using Leopotam.Ecs;
using UnityEngine;
using Utils.Interaction;

namespace Gameplay.Game.ECS.Features
{
    public class InteractionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InteractionEvent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var interactionEvent = ref filter.Get1(i);

                if(TryGetInteractor(interactionEvent, out IInteractor interactor))
                {
                    if(interactor.IsInteractable)
                        interactor.Interact();
                }
            }
        }

        private bool TryGetInteractor(InteractionEvent interactionEvent, out IInteractor interactor)
        {
            var colliders = Physics.OverlapSphere(interactionEvent.Pivot.position, interactionEvent.Range, interactionEvent.InteractorLayer);

            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].TryGetComponent(out interactor))
                    return true;
            }

            interactor = null;
            return false;
        }
    }
}
