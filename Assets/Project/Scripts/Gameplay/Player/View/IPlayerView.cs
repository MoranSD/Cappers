using Gameplay.Player.Look;
using Gameplay.Player.Movement;

namespace Gameplay.Player.View
{
    public interface IPlayerView
    {
        IPlayerMovementView MovementView { get; }
        IPlayerLook LookView { get; }
    }
}
