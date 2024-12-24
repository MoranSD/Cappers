using Leopotam.Ecs;
using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct MoveRequest
    {
        public EcsEntity Target;
        public Vector3 Direction;
        public float Speed;
    }
}
