using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct MoveEvent
    {
        public EcsEntity Target;
        public Vector3 Direction;
        public float Speed;
    }
}
