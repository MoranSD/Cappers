using UnityEngine;

namespace Gameplay.Player.Movement
{
    public class PlayerMovementView : MonoBehaviour, IPlayerMovement
    {
        [SerializeField] private CharacterController characterController;

        public Vector3 GetPosition() => transform.position;

        private void Update()
        {
            characterController.Move(Physics.gravity * Time.deltaTime);
        }

        public void Move(Vector3 direction, float speed)
        {
            direction *= speed;
            characterController.Move(direction);
        }

        public void Turn(Vector3 direction, float speed)
        {
            if (direction == Vector3.zero) return;

            direction.y = 0;

            var lookRotation = Quaternion.LookRotation(direction);
            var targetRotation = Quaternion.RotateTowards(transform.rotation, lookRotation, speed);
            transform.rotation = targetRotation;
        }
    }
}
