using UnityEngine;

namespace CameraSystem
{
    public interface IGameCamera
    {
        Vector3 Forward { get; }
        Vector3 Right { get; }
    }
}
