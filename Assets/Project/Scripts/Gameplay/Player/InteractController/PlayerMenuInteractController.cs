using Gameplay.CameraSystem;
using Gameplay.Panels;
using Infrastructure.Routine;
using System.Collections;
using UnityEngine;
using Utils.Interaction;

namespace Gameplay.Player.InteractController
{
    public class PlayerMenuInteractController
    {
        public bool IsInteracting { get; private set; }
        public PanelType InteractPanelType { get; private set; }

        private readonly PlayerController playerController;
        private readonly PanelsManager panelsManager;
        private readonly ICoroutineRunner coroutineRunner;

        private bool isCameraInteracting;
        private bool isEntering;
        private bool isExiting;

        public PlayerMenuInteractController(PlayerController playerController, PanelsManager panelsManager, ICoroutineRunner coroutineRunner)
        {
            this.playerController = playerController;
            this.panelsManager = panelsManager;
            this.coroutineRunner = coroutineRunner;
        }

        public bool CheckInteraction(PanelType panelType) => IsInteracting && InteractPanelType == panelType;

        public void EnterInteractState(PanelType panelType, ICameraFollowInteractor followInteractor = null)
        {
            if (IsInteracting || isEntering)
                throw new System.Exception($"{IsInteracting}:{isEntering}");

            IsInteracting = true;
            InteractPanelType = panelType;
            playerController.SetFreezee(true);
            
            coroutineRunner.StartCoroutine(EnterInteractStateProcess(panelType));
        }

        public void ExitInteractState()
        {
            if (IsInteracting == false || isExiting)
                throw new System.Exception($"{IsInteracting}:{isExiting}");

            IsInteracting = false;
            InteractPanelType = default;
            playerController.SetFreezee(false);

            coroutineRunner.StartCoroutine(ExitInteractStateProcess());
        }

        private IEnumerator EnterInteractStateProcess(PanelType panelType, ICameraFollowInteractor followInteractor = null)
        {
            isEntering = true;

            yield return new WaitWhile(() => isExiting);

            yield return coroutineRunner.StartCoroutine(panelsManager.ShowPanelRoutine(panelType));

            if (followInteractor != null)
            {
                isCameraInteracting = true;
                yield return coroutineRunner.StartCoroutine(playerController.GameCamera.EnterInteractStateRoutine(followInteractor.GetCameraPivot()));
            }

            isEntering = false;
        }
        private IEnumerator ExitInteractStateProcess()
        {
            isExiting = true;

            yield return new WaitWhile(() => isEntering);

            yield return coroutineRunner.StartCoroutine(panelsManager.ShowDefaultRoutine());

            if (isCameraInteracting)
            {
                isCameraInteracting = false;
                playerController.GameCamera.ExitInteractState();
            }

            isExiting = false;
        }
    }
}
