using Cysharp.Threading.Tasks;
using Gameplay.Panels;
using Utils.Interaction;

namespace Gameplay.Player.InteractController
{
    public class PlayerMenuInteractController
    {
        public bool IsInteracting { get; private set; }
        public PanelType InteractPanelType { get; private set; }

        private readonly IPlayerController playerController;
        private readonly PanelsManager panelsManager;

        private bool isCameraInteracting;
        private bool isEntering;
        private bool isExiting;

        public PlayerMenuInteractController(IPlayerController playerController, PanelsManager panelsManager)
        {
            this.playerController = playerController;
            this.panelsManager = panelsManager;
        }

        public void Dispose()
        {

        }

        public bool CheckInteraction(PanelType panelType) => IsInteracting && InteractPanelType == panelType;

        public async void EnterInteractState(PanelType panelType, ICameraFollowInteractor followInteractor = null)
        {
            if (IsInteracting || isEntering)
                throw new System.Exception($"{IsInteracting}:{isEntering}");

            IsInteracting = true;
            InteractPanelType = panelType;
            playerController.SetFreeze(true);

            await EnterInteractStateProcess(panelType, followInteractor);
        }

        public async void ExitInteractState()
        {
            if (IsInteracting == false || isExiting)
                throw new System.Exception($"{IsInteracting}:{isExiting}");

            IsInteracting = false;
            InteractPanelType = default;
            playerController.SetFreeze(false);

            await ExitInteractStateProcess();
        }

        private async UniTask EnterInteractStateProcess(PanelType panelType, ICameraFollowInteractor followInteractor = null)
        {
            isEntering = true;

            await UniTask.WaitWhile(() => isExiting);

            if (followInteractor != null)
            {
                isCameraInteracting = true;
                await playerController.GameCamera.EnterInteractStateAsync(followInteractor.GetCameraPivot());
            }

            await panelsManager.ShowPanelAsync(panelType);

            isEntering = false;
        }
        private async UniTask ExitInteractStateProcess()
        {
            isExiting = true;

            await UniTask.WaitWhile(() => isEntering);

            await panelsManager.ShowDefaultAsync();

            if (isCameraInteracting)
            {
                isCameraInteracting = false;
                playerController.GameCamera.ExitInteractState();
            }

            isExiting = false;
        }
    }
}
