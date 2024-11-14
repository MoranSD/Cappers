using UnityEngine;
using Leopotam.Ecs;

namespace Utils
{
    public interface IEcsEntityHolder
    {
        Transform transform { get; }
        EcsEntity EcsEntity { get; }
    }
}
