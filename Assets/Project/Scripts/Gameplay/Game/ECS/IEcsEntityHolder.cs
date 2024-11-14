using UnityEngine;
using Leopotam.Ecs;

namespace Gameplay.Game.ECS
{
    public interface IEcsEntityHolder
    {
        Transform transform { get; }
        EcsEntity EcsEntity { get; }
    }
}
