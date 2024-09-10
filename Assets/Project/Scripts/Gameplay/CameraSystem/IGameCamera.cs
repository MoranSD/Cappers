using UnityEngine;

namespace Gameplay.CameraSystem
{
    public interface IGameCamera
    {
        Vector3 Forward { get; }
        Vector3 Right { get; }
    }
}
