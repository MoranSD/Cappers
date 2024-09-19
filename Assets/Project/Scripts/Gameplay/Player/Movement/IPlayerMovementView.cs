using UnityEngine;

namespace Gameplay.Player.Movement
{
    public interface IPlayerMovementView
    {
        Vector3 GetPosition();
        void Move(Vector3 direction, float speed);
        void Turn(Vector3 direction, float speed);
    }
}
