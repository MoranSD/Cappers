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
        public readonly IGameCamera GameCamera;

        private readonly PlayerConfig config;
        private readonly IPlayerView view;
        private readonly IInput input;

        public PlayerController(PlayerConfig config, IPlayerView view, IInput input, IGameCamera gameCamera)
        {
            IsFreezed = false;
            GameCamera = gameCamera;

            this.config = config;
            this.view = view;
            this.input = input;
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

            Move(deltaTime);
        }

        public void SetFreezee(bool freezee)
        {
            if(IsFreezed == freezee)
                throw new System.Exception(freezee.ToString());

            IsFreezed = freezee;
        }

        private void Move(float deltaTime)
        {
            var moveInput = input.MoveInput;

            if (moveInput == UnityEngine.Vector2.zero) return;

            var moveDirection = GameCamera.Forward * moveInput.y + GameCamera.Right * moveInput.x;
            moveDirection.y = 0;
            moveDirection.Normalize();

            var moveSpeed = config.MovementConfig.MoveSpeed * deltaTime;
            var turnSpeed = config.MovementConfig.RotationSpeed * deltaTime;

            view.MovementView.Move(moveDirection, moveSpeed);
            view.MovementView.Turn(moveDirection, turnSpeed);
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
