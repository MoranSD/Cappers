using Cinemachine;
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
        [SerializeField] private float changeCameraDuration = 0.75f;

        public void Initialize()
        {
            playerFollowCamera.Priority = 1;
            interactFollowCamera.Priority = 0;
        }

        public void EnterInteractState(Vector3 followPosition)
        {
            interactFollowCamera.Follow.position = followPosition;
            playerFollowCamera.Priority = 0;
            interactFollowCamera.Priority = 1;
        }

        public void ExitInteractState()
        {
            playerFollowCamera.Priority = 1;
            interactFollowCamera.Priority = 0;
        }

        public async Task EnterInteractStateAsync(Vector3 followPosition, CancellationToken token)
        {
            EnterInteractState(followPosition);
            
            try
            {
                await Task.Delay(TimeSpan.FromSeconds(changeCameraDuration), token);
            }
            catch
            {
                return;
            }
        }
    }
}
