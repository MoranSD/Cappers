using Cysharp.Threading.Tasks;
using Gameplay.Panels;
using System.Threading;
using UnityEngine;

namespace Gameplay.Player.InteractController
{
    public class PlayerInteractController
    {
        public bool IsInteracting { get; private set; }
        public bool IsInteractingWithMenu { get; private set; }
        public PanelType InteractPanelType { get; private set; }

        private readonly IPlayerController playerController;
        private readonly PanelsManager panelsManager;

        private bool isCameraInteracting;
        private bool isEntering;
        private bool isExiting;

        private CancellationTokenSource cancellationTokenSource;

        public PlayerInteractController(IPlayerController playerController, PanelsManager panelsManager)
        {
            this.playerController = playerController;
            this.panelsManager = panelsManager;
        }

        public void Initialize()
        {
            cancellationTokenSource = new();
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }

        public bool CheckInteraction(PanelType panelType) => IsInteracting && IsInteractingWithMenu && InteractPanelType == panelType;

        //todo: ChangeInteractState() -> basically exit and then enter

        public async void EnterInteractState(Transform cameraFollowPivot) => await EnterInteractStateAsync(cameraFollowPivot);
        public async UniTask EnterInteractStateAsync(Transform cameraFollowPivot)
        {
            if (IsInteracting || isEntering)
                return;

            await EnterInteractStateProcess(cameraFollowPivot);
        }
        public async void EnterInteractState(PanelType panelType, Transform cameraFollowPivot = null) => await EnterInteractStateAsync(panelType, cameraFollowPivot);
        public async UniTask EnterInteractStateAsync(PanelType panelType, Transform cameraFollowPivot = null)
        {
            if (IsInteracting || isEntering)
                return;

            await EnterInteractStateProcess(cameraFollowPivot, true, panelType);
        }

        public async void ExitInteractState()
        {
            if (IsInteracting == false || isExiting)
                return;

            await ExitInteractStateProcess();
        }

        private async UniTask EnterInteractStateProcess(Transform cameraFollowPivot = null, bool shouldOpenMenu = false, PanelType panelType = default)
        {
            IsInteracting = true;
            isEntering = true;

            await UniTask.WaitWhile(() => isExiting, PlayerLoopTiming.Update, cancellationTokenSource.Token);

            if (cancellationTokenSource.IsCancellationRequested) return;

            playerController.SetFreeze(true);

            if (cameraFollowPivot != null)
            {
                isCameraInteracting = true;
                await playerController.GameCamera.EnterInteractStateAsync(cameraFollowPivot, cancellationTokenSource.Token);

                if (cancellationTokenSource.IsCancellationRequested) return;
            }

            if(shouldOpenMenu)
            {
                IsInteractingWithMenu = true;
                InteractPanelType = panelType;
                await panelsManager.ShowPanelAsync(panelType, cancellationTokenSource.Token);

                if (cancellationTokenSource.IsCancellationRequested) return;
            }

            isEntering = false;
        }
        private async UniTask ExitInteractStateProcess()
        {
            IsInteracting = false;
            isExiting = true;

            await UniTask.WaitWhile(() => isEntering, PlayerLoopTiming.Update, cancellationTokenSource.Token);

            if (cancellationTokenSource.IsCancellationRequested) return;

            playerController.SetFreeze(false);

            if(IsInteractingWithMenu)
            {
                IsInteractingWithMenu = false;
                InteractPanelType = default;
                await panelsManager.ShowDefaultAsync(cancellationTokenSource.Token);

                if (cancellationTokenSource.IsCancellationRequested) return;
            }

            if (isCameraInteracting)
            {
                isCameraInteracting = false;
                playerController.GameCamera.ExitInteractState();
            }

            isExiting = false;
        }
    }
}
