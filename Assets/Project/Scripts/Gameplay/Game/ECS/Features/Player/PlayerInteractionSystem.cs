using Gameplay.Player.Data;
using Infrastructure.GameInput;
using Leopotam.Ecs;
using System.Linq;
using UnityEngine;
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
        private readonly EcsFilter<TagPlayer, TranslationComponent, FollowControllerComponent>.Exclude<BlockFreezed> filter = null;

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
                    if (followControlComponent.EntitiesInControl.Count > 0 && interactor is IUnitInteractable unitInteractable)
                    {
                        var unitInteract = followControlComponent.EntitiesInControl.First();

                        _world.NewEntity()
                            .Replace(new RemoveFollowControlRequest()
                            {
                                Sender = playerEntity,
                                Target = unitInteract,
                            })
                            .Replace(new UnitInteractJobRequest()
                            {
                                Target = unitInteract,
                                Interactable = unitInteractable,
                            })
                            .Replace(new OneFrameEntity());
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

                    if (followControlComponent.EntitiesInControl.Contains(entityAround))
                    {
                        _world.NewOneFrameEntity(new RemoveFollowControlRequest()
                        {
                            Sender = playerEntity,
                            Target = entityAround,
                        });
                    }
                    else
                    {
                        _world.NewOneFrameEntity(new AddFollowControlRequest()
                        {
                            Sender = playerEntity,
                            Target = entityAround,
                        });
                    }
                }
            }
        }
    }
}
