using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public struct ApplySlowDownEvent
    {
        public EcsEntity Target;
        public float Duration;
        public float SlowSpeed;
        public bool WithSmoothRecovery;
    }
}
