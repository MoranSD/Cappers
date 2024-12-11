﻿using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.CameraSystem
{
    public interface IGameCamera
    {
        Vector3 Forward { get; }
        Vector3 Right { get; }

        void EnterFollowState(Transform target);
        void ExitFollowState();

        void EnterInteractState(Vector3 followPosition);
        UniTask EnterInteractStateAsync(Vector3 followPosition);
        void ExitInteractState();
    }
}
