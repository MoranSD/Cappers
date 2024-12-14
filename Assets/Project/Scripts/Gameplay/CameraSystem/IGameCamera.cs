using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Gameplay.CameraSystem
{
    public interface IGameCamera
    {
        Vector3 Forward { get; }
        Vector3 Right { get; }

        void EnterInteractState(Transform target);
        UniTask EnterInteractStateAsync(Transform target, CancellationToken token);
        void ExitInteractState();
    }
}
