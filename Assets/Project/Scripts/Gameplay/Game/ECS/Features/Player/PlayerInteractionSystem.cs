﻿using Gameplay.Player.Data;
using Infrastructure.GameInput;
using Leopotam.Ecs;
using System.Linq;
using Utils;
using Utils.Interaction;

namespace Gameplay.Game.ECS.Features
{
    public class PlayerInteractionSystem : IEcsRunSystem
    {
        private readonly GameConfig gameConfig = null;
        private readonly PlayerConfigSO playerConfig = null;
        private readonly IInput input = null;
        private readonly EcsWorld _world = null;
        private readonly EcsFilter<TagPlayer, TranslationComponent, FollowControlComponent>.Exclude<BlockFreezed> filter = null;

        public void Run()
        {
            if (input.IsInteractButtonPressed == false) return;

            float interactRange = playerConfig.MainConfig.LookConfig.InteractRange;

            foreach (var i in filter)
            {
                ref var transform = ref filter.Get2(i).Transform;
                ref var followControlComponent = ref filter.Get3(i);
                ref var playerEntity = ref filter.GetEntity(i);

                if (EnvironmentProvider.TryGetInteractor(transform, interactRange, out IInteractor interactor) && 
                    interactor.IsInteractable)
                {
                    if (followControlComponent.EntitiesInControl.Count > 0 && interactor is IUnitInteractable)
                    {
                        var requestEntity = _world.NewEntity();
                        ref var removeFollowRequest = ref requestEntity.Get<RemoveFollowControlRequest>();
                        removeFollowRequest.Sender = playerEntity;
                        removeFollowRequest.Target = followControlComponent.EntitiesInControl.First();
                        //unit interacts with interactor
                        interactor.Interact();
                    }
                    else
                    {
                        interactor.Interact();
                    }
                }
                else
                {
                    if (EnvironmentProvider.TryGetEntityHoldersAround
                    (transform, interactRange, gameConfig.UnitLayer, out var holdersAround) == false)
                        continue;

                    var entityAround = EnvironmentProvider.GetClosestHolder(transform.position, holdersAround).EcsEntity;
                    var entityRequest = _world.NewEntity();

                    if (followControlComponent.EntitiesInControl.Contains(entityAround))
                    {
                        ref var removeFollowRequest = ref entityRequest.Get<RemoveFollowControlRequest>();
                        removeFollowRequest.Sender = playerEntity;
                        removeFollowRequest.Target = entityAround;
                    }
                    else
                    {
                        ref var followRequest = ref entityRequest.Get<AddFollowControlRequest>();
                        followRequest.Sender = playerEntity;
                        followRequest.Target = entityAround;
                    }
                }
            }
        }
    }
}
