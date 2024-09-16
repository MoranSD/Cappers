﻿using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.CameraSystem
{
    public interface IGameCamera
    {
        Vector3 Forward { get; }
        Vector3 Right { get; }

        void EnterInteractState(Vector3 followPosition);
        Task EnterInteractStateAsync(Vector3 followPosition);
        void ExitInteractState();
    }
}
