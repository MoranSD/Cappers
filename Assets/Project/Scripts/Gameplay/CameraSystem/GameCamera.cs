using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.CameraSystem
{
    public class GameCamera : MonoBehaviour, IGameCamera
    {
        public Vector3 Forward => transform.forward;
        public Vector3 Right => transform.right;

        [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
        [SerializeField] private CinemachineVirtualCamera interactFollowCamera;
        [SerializeField] private CinemachineVirtualCamera targetFollowCamera;
        [SerializeField] private float changeCameraDuration = 0.75f;

        public void Initialize()
        {
            playerFollowCamera.Priority = 1;
            interactFollowCamera.Priority = 0;
            targetFollowCamera.Priority = 0;
        }

        public void EnterFollowState(Transform target)
        {
            targetFollowCamera.Follow = target;
            targetFollowCamera.Priority = 1;

            playerFollowCamera.Priority = 0;
            interactFollowCamera.Priority = 0;
        }

        public void ExitFollowState()
        {
            playerFollowCamera.Priority = 1;

            targetFollowCamera.Priority = 0;
            interactFollowCamera.Priority = 0;
        }

        public void EnterInteractState(Vector3 followPosition)
        {
            interactFollowCamera.Follow.position = followPosition;
            interactFollowCamera.Priority = 1;

            playerFollowCamera.Priority = 0;
            targetFollowCamera.Priority = 0;
        }

        public void ExitInteractState()
        {
            playerFollowCamera.Priority = 1;

            targetFollowCamera.Priority = 0;
            interactFollowCamera.Priority = 0;
        }

        public async UniTask EnterInteractStateAsync(Vector3 followPosition, CancellationToken token)
        {
            EnterInteractState(followPosition);
            
            try
            {
                await UniTask.Delay(TimeSpan.FromSeconds(changeCameraDuration), false, PlayerLoopTiming.Update, token);
            }
            catch
            {
                return;
            }
        }
    }
}
