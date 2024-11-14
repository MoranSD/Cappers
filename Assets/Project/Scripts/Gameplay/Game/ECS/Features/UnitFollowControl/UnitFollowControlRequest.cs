using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public struct UnitFollowControlRequest
    {
        public EcsEntity Target;
        public float Range;
    }
}
