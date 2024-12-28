using Gameplay.Player.Data;
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
                        var unitInteract = EntityUtil.GetClosestEntity(ref transform, ref followControlComponent.EntitiesInControl);

                        EventBus.Invoke(new RemoveFollowControlRequest()
                        {
                            Sender = playerEntity,
                            Target = unitInteract,
                        });
                        EventBus.Invoke(new UnitInteractJobRequest()
                        {
                            Target = unitInteract,
                            Interactable = unitInteractable,
                        });
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
                        EventBus.Invoke(new RemoveFollowControlRequest()
                        {
                            Sender = playerEntity,
                            Target = entityAround,
                        });
                    }
                    else
                    {
                        EventBus.Invoke(new AddFollowControlRequest()
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
