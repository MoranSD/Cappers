using Gameplay.CameraSystem;
using Infrastructure.TickManagement;
using Gameplay.Player.Data;
using Gameplay.Player.View;
using Infrastructure.GameInput;

namespace Gameplay.Player
{
    public class PlayerController : ITickable
    {
        public bool IsFreezed { get; private set; }

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
            input.OnPressInteractButton += OnInteract;
        }

        public void Dispose()
        {
            input.OnPressInteractButton -= OnInteract;
        }

        public void Update(float deltaTime)
        {
            if (IsFreezed) return;

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

        public void SetFreezee(bool freezee)
        {
            if(IsFreezed == freezee)
                throw new System.Exception(freezee.ToString());

            IsFreezed = freezee;
        }

        private void OnInteract()
        {
            if (IsFreezed) return;

            if (view.LookView.TryGetInteractor(config.LookConfig.InteractRange, out var interactor))
                if (interactor.IsInteractable)
                    interactor.Interact();
        }
    }
}
