﻿using UnityEngine;

namespace CameraSystem
{
    public class GameCamera : MonoBehaviour, IGameCamera
    {
        public Vector3 Forward => transform.forward;
        public Vector3 Right => transform.right;
    }
}
