using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public struct ApplyDamageEvent
    {
        public EcsEntity Sender;
        public EcsEntity Target;
        public float Damage;
    }
}
