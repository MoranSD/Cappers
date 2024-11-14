using Leopotam.Ecs;
using Utils;
using Utils.Interaction;

namespace Gameplay.Game.ECS.Features
{
    public class InteractionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<InteractionRequest> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var interactionRequest = ref filter.Get1(i);
                ref var transform = ref interactionRequest.Target.Get<TranslationComponent>().Transform;

                if (EnvironmentProvider.TryGetInteractor(transform, interactionRequest.Range, out IInteractor interactor))
                {
                    if(interactor.IsInteractable)
                        interactor.Interact();
                }
            }
        }
    }
}
