using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public struct ApplyDamageRequest
    {
        public EcsEntity Sender;
        public EcsEntity Target;
        public float Damage;
    }
}
