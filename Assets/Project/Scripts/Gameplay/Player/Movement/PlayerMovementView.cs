using UnityEngine;

namespace Gameplay.Player.Movement
{
    public class PlayerMovementView : MonoBehaviour, IPlayerMovementView
    {
        [SerializeField] private CharacterController characterController;

        public Vector3 GetPosition() => transform.position;

        public void Move(Vector3 direction, float speed)
        {
            direction *= speed;
            var gravityForce = Physics.gravity * Time.deltaTime;

            characterController.Move(direction + gravityForce);
        }

        public void Turn(Vector3 direction, float speed)
        {
            var lookRotation = Quaternion.LookRotation(direction);
            var targetRotation = Quaternion.RotateTowards(transform.rotation, lookRotation, speed);
            transform.rotation = targetRotation;
        }
    }
}
