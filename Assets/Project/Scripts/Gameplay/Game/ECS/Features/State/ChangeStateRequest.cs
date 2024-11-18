using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public struct ChangeStateRequest
    {
        public delegate void ChangeStateDelegate(ref EcsEntity entity);

        public EcsEntity Target;
        public ChangeStateDelegate Delegate;
    }
}
