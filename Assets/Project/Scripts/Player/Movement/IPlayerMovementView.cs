using UnityEngine;

namespace Player.Movement
{
    public interface IPlayerMovementView
    {
        void Move(Vector2 input, float speed);
    }
}
