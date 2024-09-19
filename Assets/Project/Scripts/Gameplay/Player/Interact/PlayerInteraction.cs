namespace Gameplay.Player.Interact
{
    public class PlayerInteraction
    {
        public bool HandleInput;

        private readonly PlayerController controller;

        public PlayerInteraction(PlayerController controller)
        {
            this.controller = controller;
        }

        public void Initialize()
        {
            controller.Input.OnPressInteractButton += OnInteract;
        }

        public void Dispose()
        {
            controller.Input.OnPressInteractButton -= OnInteract;
        }

        private void OnInteract()
        {
            if (HandleInput == false) return;

            if (controller.View.Look.TryGetInteractor(controller.Config.LookConfig.InteractRange, out var interactor))
                if (interactor.IsInteractable)
                    interactor.Interact();
        }
    }
}
