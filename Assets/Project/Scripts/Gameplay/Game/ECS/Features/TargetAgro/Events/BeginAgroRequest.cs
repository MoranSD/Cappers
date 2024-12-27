using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public struct BeginAgroRequest
    {
        public EcsEntity Entity;
        public EcsEntity Target;
    }
}
