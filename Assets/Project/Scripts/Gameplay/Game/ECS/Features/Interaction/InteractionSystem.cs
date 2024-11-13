using Leopotam.Ecs;
using Utils;
using Utils.Interaction;

namespace Gameplay.Game.ECS.Features
{
    public class InteractionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TranslationComponent, InteractionEvent> filter = null;

        public void Run()
        {
            foreach (var i in filter)
            {
                ref var transform = ref filter.Get1(i).Transform;
                ref var interactionEvent = ref filter.Get2(i);

                if (EnvironmentProvider.TryGetInteractor(transform, interactionEvent.Range, out IInteractor interactor))
                {
                    if(interactor.IsInteractable)
                        interactor.Interact();
                }
            }
        }
    }
}
