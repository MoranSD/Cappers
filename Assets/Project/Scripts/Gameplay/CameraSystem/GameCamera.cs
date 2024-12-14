using Cinemachine;
using Cysharp.Threading.Tasks;
using System;
using System.Threading;
using UnityEngine;

namespace Gameplay.CameraSystem
{
    public class GameCamera : MonoBehaviour, IGameCamera
    {
        public Vector3 Forward => transform.forward;
        public Vector3 Right => transform.right;

        [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
        [SerializeField] private CinemachineVirtualCamera targetFollowCamera;
        [SerializeField] private float changeCameraDuration = 0.75f;

        public void Initialize()
        {
            playerFollowCamera.Priority = 1;
            targetFollowCamera.Priority = 0;
        }

        public void EnterInteractState(Transform target)
        {
            targetFollowCamera.Follow = target;
            targetFollowCamera.LookAt = target;
            targetFollowCamera.Priority = 1;

            playerFollowCamera.Priority = 0;
        }

        public void ExitInteractState()
        {
            playerFollowCamera.Priority = 1;
            targetFollowCamera.Priority = 0;
        }

        public async UniTask EnterInteractStateAsync(Transform target, CancellationToken token)
        {
            EnterInteractState(target);
            
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
