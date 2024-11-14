using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public struct AddFollowControlRequest
    {
        public EcsEntity Sender;
        public EcsEntity Target;
    }
}
