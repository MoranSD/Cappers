using Gameplay.Player;
using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public class PlayerInteractionInputSystem : IEcsRunSystem
    {
        private readonly PlayerController playerController = null;
        private readonly EcsFilter<TagPlayer> filter = null;

        public void Run()
        {
            if (playerController.Input.IsInteractButtonPressed == false) return;

            foreach (var i in filter)
            {
                ref var entity = ref filter.GetEntity(i);
                ref var interactionEvent = ref entity.Get<InteractionEvent>();

                interactionEvent.Pivot = playerController.Pivot;
                interactionEvent.InteractorLayer = playerController.InteractorLayer;
                interactionEvent.Range = playerController.Config.LookConfig.InteractRange;
            }
        }
    }
}
