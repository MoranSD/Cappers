using CameraSystem;
using Infrastructure.TickManagement;
using Player.Data;
using Player.View;
using Services.GameInput;
using Unity.VisualScripting;

namespace Player
{
    public class PlayerController : ITickable
    {
        private readonly PlayerConfig config;
        private readonly IPlayerView view;
        private readonly IInput input;
        private readonly IGameCamera gameCamera;

        public PlayerController(PlayerConfig config, IPlayerView view, IInput input, IGameCamera gameCamera)
        {
            this.config = config;
            this.view = view;
            this.input = input;
            this.gameCamera = gameCamera;
        }

        public void Initialize()
        {

        }

        public void Update(float deltaTime)
        {
            var moveInput = input.MoveInput;

            if (moveInput == UnityEngine.Vector2.zero) return;

            var moveDirection = gameCamera.Forward * moveInput.y + gameCamera.Right * moveInput.x;
            moveDirection.y = 0;
            moveDirection.Normalize();

            var moveSpeed = config.MovementConfig.MoveSpeed * deltaTime;
            var turnSpeed = config.MovementConfig.RotationSpeed * deltaTime;

            view.MovementView.Move(moveDirection, moveSpeed);
            view.MovementView.Turn(moveDirection, turnSpeed);
        }

        public void Dispose()
        {

        }
    }
}
