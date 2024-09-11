using Gameplay.CameraSystem;
using Infrastructure.Panels;
using Utils.Interaction;

namespace Gameplay.Player.InteractController
{
    public class PlayerMenuInteractController
    {
        public bool IsInteracting { get; private set; }
        public PanelType InteractPanelType { get; private set; }

        private readonly PlayerController playerController;
        private readonly PanelsManager panelsManager;
        private readonly IGameCamera gameCamera;

        private bool isCameraInteracting;

        public PlayerMenuInteractController(PlayerController playerController, PanelsManager panelsManager, IGameCamera gameCamera)
        {
            this.playerController = playerController;
            this.panelsManager = panelsManager;
            this.gameCamera = gameCamera;
        }

        public void EnterInteractState(PanelType panelType, ICameraFollowInteractor followInteractor = null)
        {
            if (playerController.IsFreezed)
                throw new System.Exception();

            IsInteracting = true;
            InteractPanelType = panelType;
            playerController.SetFreezee(true);
            panelsManager.ShowPanel(panelType);

            if(followInteractor != null)
            {
                isCameraInteracting = true;
                gameCamera.EnterInteractState(followInteractor.GetCameraPivot());
            }
        }

        public void ExitInteractState()
        {
            if (playerController.IsFreezed == false)
                throw new System.Exception();

            IsInteracting = false;
            InteractPanelType = default;
            playerController.SetFreezee(false);
            panelsManager.ShowDefault();

            if (isCameraInteracting)
            {
                isCameraInteracting = false;
                gameCamera.ExitInteractState();
            }
        }
    }
}
