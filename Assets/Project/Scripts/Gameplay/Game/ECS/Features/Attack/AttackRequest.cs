using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public struct AttackRequest
    {
        public EcsEntity Sender;
        public EcsEntity Target;
    }
}
