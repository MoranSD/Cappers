using Gameplay.CameraSystem;
using UnityEngine;

namespace Gameplay.Player
{
    public interface IPlayerController
    {
        Transform Pivot { get; }
        IGameCamera GameCamera { get; }
        void SetFreeze(bool freeze);
    }
}
