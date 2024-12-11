using Cysharp.Threading.Tasks;
using Gameplay.Panels;
using System.Threading;
using System.Threading.Tasks;
using Utils;
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

        private CancellationTokenSource cancellationTokenSource;

        public PlayerMenuInteractController(IPlayerController playerController, PanelsManager panelsManager)
        {
            this.playerController = playerController;
            this.panelsManager = panelsManager;
        }

        public void Dispose()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
            }
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
            cancellationTokenSource = new CancellationTokenSource();

            await UniTask.WaitWhile(() => isExiting, PlayerLoopTiming.Update, cancellationTokenSource.Token);

            if (cancellationTokenSource.IsCancellationRequested) return;

            if (followInteractor != null)
            {
                isCameraInteracting = true;
                await playerController.GameCamera.EnterInteractStateAsync(followInteractor.GetCameraPivot(), cancellationTokenSource.Token);

                if (cancellationTokenSource.IsCancellationRequested) return;
            }

            await panelsManager.ShowPanelAsync(panelType, cancellationTokenSource.Token);

            if (cancellationTokenSource.IsCancellationRequested) return;

            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
            isEntering = false;
        }
        private async UniTask ExitInteractStateProcess()
        {
            isExiting = true;
            cancellationTokenSource = new CancellationTokenSource();

            await UniTask.WaitWhile(() => isEntering, PlayerLoopTiming.Update, cancellationTokenSource.Token);

            if (cancellationTokenSource.IsCancellationRequested) return;

            await panelsManager.ShowDefaultAsync(cancellationTokenSource.Token);

            if (cancellationTokenSource.IsCancellationRequested) return;

            if (isCameraInteracting)
            {
                isCameraInteracting = false;
                playerController.GameCamera.ExitInteractState();
            }

            cancellationTokenSource.Dispose();
            cancellationTokenSource = null;
            isExiting = false;
        }
    }
}
