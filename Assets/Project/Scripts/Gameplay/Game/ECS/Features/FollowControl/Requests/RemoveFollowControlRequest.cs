using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public struct RemoveFollowControlRequest
    {
        public EcsEntity Sender;
        public EcsEntity Target;
    }
}
