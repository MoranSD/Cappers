﻿using Cinemachine;
using System.Collections;
using UnityEngine;

namespace Gameplay.CameraSystem
{
    public class GameCamera : MonoBehaviour, IGameCamera
    {
        public Vector3 Forward => transform.forward;
        public Vector3 Right => transform.right;

        [SerializeField] private CinemachineVirtualCamera playerFollowCamera;
        [SerializeField] private CinemachineVirtualCamera interactFollowCamera;
        [SerializeField] private float changeCameraDuration = 1.75f;

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

        public IEnumerator EnterInteractStateRoutine(Vector3 followPosition)
        {
            EnterInteractState(followPosition);
            yield return new WaitForSeconds(changeCameraDuration);
        }
    }
}