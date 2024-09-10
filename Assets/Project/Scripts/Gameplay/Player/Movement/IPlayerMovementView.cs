using UnityEngine;

namespace Gameplay.Player.Movement
{
    public interface IPlayerMovementView
    {
        void Move(Vector3 direction, float speed);
        void Turn(Vector3 direction, float speed);
    }
}
