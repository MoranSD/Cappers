using Leopotam.Ecs;

namespace Gameplay.Game.ECS.Features
{
    public interface IAnimationUpdater
    {
        void Update(ref EcsEntity ecsEntity);
    }
}
