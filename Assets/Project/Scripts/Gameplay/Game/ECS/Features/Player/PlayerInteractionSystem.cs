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
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TagPlayer, TranslationComponent>.Exclude<BlockFreezed> filter = null;

        public void Run()
        {
            if (input.IsInteractButtonPressed == false) return;

            float interactRange = playerConfig.MainConfig.LookConfig.InteractRange;

            foreach (var i in filter)
            {
                ref var transform = ref filter.Get2(i).Transform;

                ref var playerEntity = ref filter.GetEntity(i);
                var requestEntity = _world.NewEntity();

                if (EnvironmentProvider.HasInteractorAround(transform, interactRange))
                {
                    ref var interactionRequest = ref requestEntity.Get<InteractionRequest>();

                    interactionRequest.Target = playerEntity;
                    interactionRequest.Range = interactRange;
                }
                else
                {
                    ref var unitFollowControlRequest = ref requestEntity.Get<UnitFollowControlRequest>();

                    unitFollowControlRequest.Target = playerEntity;
                    unitFollowControlRequest.Range = interactRange;
                }
            }
        }
    }
}
