using Leopotam.Ecs;
using Utils.Interaction;

namespace Gameplay.Game.ECS.Features
{
    public struct UnitInteractJobRequest
    {
        public EcsEntity Target;
        public IUnitInteractable Interactable;
    }
}
