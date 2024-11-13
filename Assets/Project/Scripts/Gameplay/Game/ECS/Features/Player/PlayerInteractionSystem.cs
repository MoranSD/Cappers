using Gameplay.Player.Data;
using Infrastructure.GameInput;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Gameplay.Game.ECS.Features
{
    public class PlayerInteractionSystem : IEcsRunSystem
    {
        private readonly PlayerConfigSO playerConfig = null;
        private readonly IInput input = null;
        private readonly EcsFilter<TagPlayer, TranslationComponent>.Exclude<BlockFreezed> filter = null;

        public void Run()
        {
            if (input.IsInteractButtonPressed == false) return;

            float interactRange = playerConfig.MainConfig.LookConfig.InteractRange;

            foreach (var i in filter)
            {
                ref var transform = ref filter.Get2(i).Transform;

                ref var entity = ref filter.GetEntity(i);
                
                if(EnvironmentProvider.HasInteractorAround(transform, interactRange))
                {
                    ref var interactionEvent = ref entity.Get<InteractionEvent>();
                    interactionEvent.Range = interactRange;
                }
                else
                {
                    ref var unitFollowControlEvent = ref entity.Get<UnitFollowControlEvent>();
                    unitFollowControlEvent.Range = interactRange;
                }
            }
        }
    }
}
