using UnityEngine;

namespace Gameplay.Game.ECS.Features
{
    public struct ChMovableComponent
    {
        public CharacterController CharacterController;
        public Vector3 Direction;
        public float Speed;
    }
}
