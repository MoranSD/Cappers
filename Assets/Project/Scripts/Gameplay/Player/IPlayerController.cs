using Gameplay.CameraSystem;
using Gameplay.Player.Data;
using Infrastructure.GameInput;
using UnityEngine;

namespace Gameplay.Player
{
    public interface IPlayerController
    {
        Transform Pivot { get; }
        PlayerConfig Config { get; }
        IInput Input { get; }
        IGameCamera GameCamera { get; }
        void SetFreeze(bool freeze);
    }
}
