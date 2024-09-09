using CameraSystem;
using Infrastructure;
using UnityEngine;

namespace Player.Movement
{
    public class PlayerMovementView : MonoBehaviour, IPlayerMovementView
    {
        [SerializeField] private CharacterController characterController;

        private GameCamera gameCamera;

        private void Start()
        {
            gameCamera = ServiceLocator.Get<GameCamera>();
        }

        public void Move(Vector2 input, float speed)
        {
            var moveDirection = gameCamera.transform.forward * input.y + gameCamera.transform.right * input.x;
            moveDirection.y = 0;
            moveDirection.Normalize();
            moveDirection *= speed;

            var gravityForce = Physics.gravity * Time.deltaTime;

            characterController.Move(moveDirection + gravityForce);
        }
    }
}
